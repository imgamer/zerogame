using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

using System.IO;

public class PackTest
{
	[MenuItem("BundleTools/Test")]
	public static void Test()
	{
        Debug.LogError ("Application.dataPath:" + Application.dataPath);
        Debug.LogError("Application.dataPath:!/assets" + Application.dataPath + "!/Assets/");

        Debug.LogError("Application.streamingAssetsPath:" + Application.streamingAssetsPath);

        //Debug.LogError("Application.persistentDataPath:" + Application.persistentDataPath);
        //Debug.LogError("Application.temporaryCachePath:" + Application.temporaryCachePath);

        List<string> paths = GetPaths("Assets/StreamingAssets");
        foreach(string path in paths)
        {
            //Debug.LogError("getPath:"+path);
        }

	}

    private static List<string> GetPaths(string path)
    {
        List<string> ret = new List<string>();

        if (string.IsNullOrEmpty(path)) return ret;
        string[] paths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        foreach (var p in paths)
        {
            string str = p.Replace('\\', '/');
            ret.Add(str);
        }
        return ret;
    }
}


