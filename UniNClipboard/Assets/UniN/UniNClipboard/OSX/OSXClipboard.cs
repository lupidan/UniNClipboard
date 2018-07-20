using System.Runtime.InteropServices;

namespace UniN.UniNClipboard
{
	public class OSXClipboard : IClipboard
	{
		public bool ClipboardAvailable
		{
			get { return true; }
		}

		public string Text
		{
			get { return OSXUniNClipboardGetText(); }
			set { OSXUniNClipboardSetText(value); }
		}

		[DllImport("UniNClipboard")]
		private static extern string OSXUniNClipboardGetText();

		[DllImport("UniNClipboard")]
		private static extern void OSXUniNClipboardSetText(string text);
	}
}

