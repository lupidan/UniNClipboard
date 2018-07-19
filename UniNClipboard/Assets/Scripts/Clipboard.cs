#if UNITY_EDITOR
using UnityEngine;
#endif

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
                _shared = new IOSClipboard();
                #elif UNITY_ANDROID
                _shared = new AndroidClipboard("UniNClipboard");
                #endif
            }

            return _shared;
        }

    }
}
