using UnityEngine;
using UnityEditor;
using System.Collections;


public class PackTools
{
    [MenuItem("BundleTools/Resources方式打包(默认)")]
    public static void PackResources()
    {
        if (!EditorUtility.DisplayDialog("Resources方式打包", "是否开始打包", "Yes", "No"))
        {
            return;
        }

		Pack (AssetsType.Resources);

        EditorUtility.DisplayDialog("完成", "资源打包完成，请检查Console窗口是否有错误提示", "OK");
    }

	[MenuItem("BundleTools/Local Bunlde方式打包")]
	public static void PackLoaclBundle()
	{
        if (!EditorUtility.DisplayDialog("本地Bundle打包", "是否开始打包", "Yes", "No"))
        {
            return;
        }

		Pack (AssetsType.Local);

        EditorUtility.DisplayDialog("完成", "资源打包完成，请检查Console窗口是否有错误提示", "OK");
	}

	[MenuItem("BundleTools/Network Bunlde方式打包")]
	public static void PackNetworkBunlde()
	{
		Pack (AssetsType.Server);
	}

	private static Packer CreatePacker(AssetsType p_assetsType)
	{
		switch (p_assetsType) 
		{
			case AssetsType.Local:
                return new BundlePacker();
			case AssetsType.Resources:
				return new ResourcesPacker ();
			default:
				Debug.LogError (string.Format("cant find AssetsType:{0} packer.", p_assetsType.ToString()));
				return null;
		}
	}

	private static void Pack(AssetsType p_assetsType)
	{
		Packer packer = CreatePacker (p_assetsType);
		PackDirector director = new PackDirector( packer );
		director.Pack();
	}
}
