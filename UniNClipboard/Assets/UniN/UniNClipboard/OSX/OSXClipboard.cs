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

		public event Action OnClipboardChanged;

		[DllImport("UniNClipboard")]
		private static extern string OSXUniNClipboardGetText();

		[DllImport("UniNClipboard")]
		private static extern void OSXUniNClipboardSetText(string text);
	}
}

