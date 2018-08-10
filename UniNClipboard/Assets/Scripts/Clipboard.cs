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
                var gameObject = new GameObject
                {
                    name = "UniNClipboard"
                };

                Object.DontDestroyOnLoad(gameObject);
                #if UNITY_EDITOR
                _shared = gameObject.AddComponent<EditorClipboard>();
                #elif UNITY_IOS
                _shared = gameObject.AddComponent<IOSClipboard>();
                #elif UNITY_ANDROID
                _shared = gameObject.AddComponent<AndroidClipboard>();
                #elif UNITY_STANDALONE_OSX
                _shared = gameObject.AddComponent<OSXClipboard>();
                #endif
            }

            return _shared;
        }

    }
}
