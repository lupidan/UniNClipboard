using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour
{
	[SerializeField] private Text _contentLabel;

	private int listenerId = 1;
	private readonly List<Action> registeredListeners = new List<Action>();

	public void OnCopyButtonPressed()
	{
		Clipboard.SharedClipboard.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
	}

	public void OnDisplayContentPressed()
	{
		this._contentLabel.text = Clipboard.SharedClipboard.Text;
	}

	public void OnAddListenerButtonPressed()
	{
		var assignedListenerId = this.listenerId;
		this.listenerId += 1;

		Action listenerToAdd = () =>
		{
			var localListenerId = assignedListenerId;
			Debug.Log(string.Format("[{0}]Clipboard changed: {1}", localListenerId, Clipboard.SharedClipboard.Text));
		};
		Clipboard.SharedClipboard.OnClipboardChanged += listenerToAdd;
		this.registeredListeners.Add(listenerToAdd);
	}

	public void OnRemoveListenerButtonPressed()
	{
		if (this.registeredListeners.Count == 0)
			return;

		var listenerToRemove = this.registeredListeners[this.registeredListeners.Count - 1];
		Clipboard.SharedClipboard.OnClipboardChanged -= listenerToRemove;
		this.registeredListeners.Remove(listenerToRemove);
	}
}
