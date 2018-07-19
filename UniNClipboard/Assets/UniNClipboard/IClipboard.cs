
namespace UniN.Clipboard
{
	public interface IClipboard
	{
		bool ClipboardAvailable { get; }
		string Text { get; set; }
	}
}
