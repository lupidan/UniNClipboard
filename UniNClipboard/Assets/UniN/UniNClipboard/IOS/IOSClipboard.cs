using System;
using System.Runtime.InteropServices;
using AOT;

namespace UniN.UniNClipboard
{
	public class IOSClipboard : IClipboard
	{
		private static IOSClipboard sharedClipboard;

		public bool ClipboardAvailable
		{
			get { return true; }
		}

		public string Text
		{
			get { return IOSUniNClipboardGetText(); }
			set { IOSUniNClipboardSetText(value); }
		}

		private event Action onClipboardChanged;
		public event Action OnClipboardChanged
		{
			add
			{
				onClipboardChanged += value;
				sharedClipboard = this;
				IOSUniNClipboardSetChangedCallback(UniNClipboardHelperChangedCallback);
			}
			remove
			{
				onClipboardChanged -= value;
			}
		}

		[DllImport("__Internal")]
		private static extern string IOSUniNClipboardGetText();

		[DllImport("__Internal")]
		private static extern void IOSUniNClipboardSetText(string text);

		[DllImport("__Internal")]
		private static extern void IOSUniNClipboardSetChangedCallback(UniNClipboardHelperChangedDelegate changedCallback);


		private delegate void UniNClipboardHelperChangedDelegate();

		[MonoPInvokeCallback(typeof(UniNClipboardHelperChangedDelegate))]
		private static void UniNClipboardHelperChangedCallback()
		{
			sharedClipboard.onClipboardChanged.Invoke();
		}
	}
}

