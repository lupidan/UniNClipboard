using System;
using UnityEngine;

namespace UniN.UniNClipboard
{
	public class EditorClipboard : MonoBehaviour, IClipboard
	{
		[SerializeField] private bool _clipboardAvailable = true;
		[SerializeField] private string _text = "This is a test text";

		public bool ClipboardAvailable
		{
			get { return this._clipboardAvailable; }
		}

		public string Text
		{
			get { return this._text; }
			set { this._text = value; }
		}

		public event Action OnClipboardChanged;
	}
}

