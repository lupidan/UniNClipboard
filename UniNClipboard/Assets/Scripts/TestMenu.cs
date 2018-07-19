using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour
{
	[SerializeField] private Text _contentLabel;

	public void OnCopyButtonPressed()
	{
		Clipboard.SharedClipboard.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
	}

	public void OnDisplayContentPressed()
	{
		this._contentLabel.text = Clipboard.SharedClipboard.Text;
	}
}
