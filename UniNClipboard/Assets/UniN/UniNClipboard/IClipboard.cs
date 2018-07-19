
namespace UniN.UniNClipboard
{
	public interface IClipboard
	{
		bool ClipboardAvailable { get; }
		string Text { get; set; }
	}
}
