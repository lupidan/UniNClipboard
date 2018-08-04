#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

typedef void (*UniNClipboardHelperChangedDelegate)();

@interface UniNClipboardHelper : NSObject

+ (instancetype) sharedHelper;
@property (nonatomic, strong) id changedObserver;

@end

@implementation UniNClipboardHelper

static dispatch_once_t onceToken;
static UniNClipboardHelper *_sharedHelper = nil;

+ (instancetype) sharedHelper
{
    dispatch_once(&onceToken, ^{
        _sharedHelper = [[UniNClipboardHelper alloc] init];
    });
    
    return _sharedHelper;
}

@end

extern "C" {
    
    const char *IOSUniNClipboardGetText()
    {
        NSString *textContent = [[UIPasteboard generalPasteboard] string];
        if (!textContent)
            return NULL;
        
        // Allocate string in here
        // It should be freed automatically on the IL2CPP generated wrapper method => il2cpp_codegen_marshal_free
        const char *utf8TextContentString = [textContent UTF8String];
        char *utf8TextContentStringCopy = (char*)malloc(strlen(utf8TextContentString) + 1);
        strcpy(utf8TextContentStringCopy, utf8TextContentString);
        return utf8TextContentStringCopy;
    }
    
    void IOSUniNClipboardSetText(const char * text)
    {
        NSString *textContent = text != NULL ? [NSString stringWithUTF8String:text] : nil;
        [[UIPasteboard generalPasteboard] setString:textContent];
    }
    
    void IOSUniNClipboardSetChangedCallback(UniNClipboardHelperChangedDelegate changedCallback)
    {
        [[NSNotificationCenter defaultCenter] removeObserver:[[UniNClipboardHelper sharedHelper] changedObserver]];
        [[UniNClipboardHelper sharedHelper] setChangedObserver:nil];
        
        if (changedCallback != NULL)
        {
            id observer = [[NSNotificationCenter defaultCenter] addObserverForName:UIPasteboardChangedNotification
                                                                            object:nil
                                                                             queue:[NSOperationQueue mainQueue]
                                                                        usingBlock:^(NSNotification * _Nonnull note) {
                                                                            changedCallback();
                                                                        }];
            [[UniNClipboardHelper sharedHelper] setChangedObserver:observer];
        }
    }
}
