using UnityEngine;
using UnityEditor;
using System.Collections;


public class PackTools
{
    [MenuItem("BundleTools/Resources方式打包(默认)")]
    public static void PackResources()
    {
		Pack (AssetsType.Resources);
    }

	[MenuItem("BundleTools/Local Bunlde方式打包")]
	public static void PackLoaclBundle()
	{
		Pack (AssetsType.Local);
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
