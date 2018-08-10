using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace UniN.UniNClipboard
{
	public class OSXClipboard : MonoBehaviour, IClipboard
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

		private string _textOnPaused;

		private void OnApplicationPause(bool isPaused)
		{
			if (isPaused)
			{
				this._textOnPaused = Text;
			}
			else
			{
				if (this.Text != this._textOnPaused && this.OnClipboardChanged != null)
					this.OnClipboardChanged.Invoke();

				this._textOnPaused = null;
			}
		}

		[DllImport("UniNClipboard")]
		private static extern string OSXUniNClipboardGetText();

		[DllImport("UniNClipboard")]
		private static extern void OSXUniNClipboardSetText(string text);
	}
}

