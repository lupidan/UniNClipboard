using UnityEngine;

using UniN.UniNClipboard;

public class Clipboard
{
    private static IClipboard _shared;
    public static IClipboard SharedClipboard
    {
        get
        {
            if (_shared == null)
            {
                #if UNITY_EDITOR
                var gameObject = new GameObject
                {
                    name = "UniNClipboard"
                };

                Object.DontDestroyOnLoad(gameObject);
                _shared = gameObject.AddComponent<EditorClipboard>();
                #elif UNITY_IOS
                var gameObject = new GameObject
                {
                    name = "UniNClipboardIOS"
                };

                Object.DontDestroyOnLoad(gameObject);
                _shared = gameObject.AddComponent<IOSClipboard>();
                #elif UNITY_ANDROID
                var gameObject = new GameObject
                {
                    name = "UniNClipboardAndroid"
                };

                Object.DontDestroyOnLoad(gameObject);
                _shared = gameObject.AddComponent<AndroidClipboard>();
                #elif UNITY_STANDALONE_OSX
                _shared = new OSXClipboard();
                #endif
            }

            return _shared;
        }

    }
}
