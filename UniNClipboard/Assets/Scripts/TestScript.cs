
using System;
using System.Globalization;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			Clipboard.SharedClipboard.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			Debug.Log("This is on the clipboard" + Clipboard.SharedClipboard.Text);
		}
	}
}
