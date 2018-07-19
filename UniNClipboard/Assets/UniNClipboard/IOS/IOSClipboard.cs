using System.Runtime.InteropServices;

namespace UniN.Clipboard
{
	public class IOSClipboard : IClipboard
	{
		public bool ClipboardAvailable { get; private set; }

		public string Text
		{
			get { return IOSUniNClipboardGetText(); }
			set { IOSUniNClipboardSetText(value); }
		}

		[DllImport("__Internal")]
		private static extern string IOSUniNClipboardGetText();

		[DllImport("__Internal")]
		private static extern void IOSUniNClipboardSetText(string text);
	}
}

