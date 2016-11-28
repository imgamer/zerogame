using UnityEngine;
using UnityEditor;
using System.Collections;

public class PackTools
{
    [MenuItem("BundleTools/Resources方式打包(默认)")]
    public static void PackResources()
    {
        ResourcesPacker resPacker = new ResourcesPacker();
        PackDirector director = new PackDirector( resPacker );
        director.Pack();
    }

}
