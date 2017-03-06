using UnityEditor;
using UnityEngine;
using System.Collections;

using System.IO;

public class PackTest
{
	[MenuItem("BundleTools/Test")]
	public static void Test()
	{
		Debug.LogWarning ("Application.dataPath:" + Application.dataPath);

		DirectoryInfo dir_info = new DirectoryInfo ("Assets/Game/GameAssets/BundleAssets");
		FileInfo[] file_array = dir_info.GetFiles ();
		for (int i = 0; i < file_array.Length; ++i)
		{
			FileInfo file = file_array[i];
			Debug.Log("file name:" + file.Name);
		}

	}
}


