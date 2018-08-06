using UnityEngine;
using UnityEditor;

namespace UniN.UniNClipboard
{
	public class UnityPackageMenu : MonoBehaviour
	{
		[MenuItem("UniN/UniNClipboard/Generate unitypackage")]
		private static void NewMenuOption()
		{
			AssetDatabase.ExportPackage(
				"Assets/UniN/UniNClipboard",
				"../UniNClipboard.unitypackage",
				ExportPackageOptions.Recurse);
		}
	}
}
