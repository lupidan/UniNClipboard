#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

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
}
