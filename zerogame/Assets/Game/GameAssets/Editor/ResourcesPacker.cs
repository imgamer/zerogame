using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;


public class ResourcesPacker : Packer 
{
	
    public ResourcesPacker()
    {
        m_assetsType = AssetsType.Resources;
    }
	
	protected override List<string> GetAssetsPaths()
	{
		List<string> assetsList = new List<string> ();
		assetsList.Add(SCENE_FILE_PATH);
		assetsList.Add(string.Format("{0}/{1}", FIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
		assetsList.Add(string.Format( "{0}/{1}", UNFIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
		return assetsList;
	}

	public override void SetSencesInBuild() 
    {
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++ )
        {
            EditorBuildSettings.scenes[i].enabled = true;
        }
    }

	public override void PackAssets() 
    {
        AssetDatabase.RenameAsset(string.Format("{0}/{1}", UNFIXED_ASSETS_PATH, PACKAGES_DIR_NAME), RESOURCES_DIR_NAME);
        AssetDatabase.Refresh();

		m_assetsDetailDict.Add (ASSETS_CONFIG_FILE_NAME, new AssetsDetail ( ASSETS_CONFIG_FILE_NAME, AssetsType.Resources, string.Empty, 0, 0 ));
		m_assetsDetailDict.Add (BUNDLE_FILE_NAME, new AssetsDetail ( BUNDLE_FILE_NAME, AssetsType.Resources, string.Empty, 0, 0 ));

        UpdateDetailDict();
        CreateConfigTableFile();
        UpdateConfigTableBundleBuild();

        BuildAssetsBundle();
	}

	public override void DistributeAssets() 
    {
		// assets_table
		FileInfo fileInfo = new FileInfo (Path.Combine (BUNDLE_ASSETS_PATH, ASSETS_CONFIG_FILE_NAME));
		fileInfo.MoveTo (Path.Combine(STREAMING_ASSETS_PATH, ASSETS_CONFIG_FILE_NAME ));
		string manifest_file_name = ASSETS_CONFIG_FILE_NAME + ".manifest";
		fileInfo = new FileInfo (Path.Combine (BUNDLE_ASSETS_PATH, manifest_file_name));
		fileInfo.MoveTo (Path.Combine(STREAMING_ASSETS_PATH, manifest_file_name ));

		// BundleAssets
		fileInfo = new FileInfo (Path.Combine( BUNDLE_ASSETS_PATH, BUNDLE_FILE_NAME ));
		fileInfo.MoveTo (Path.Combine(STREAMING_ASSETS_PATH, BUNDLE_FILE_NAME ));
		manifest_file_name = BUNDLE_FILE_NAME + ".manifest";
		fileInfo = new FileInfo (Path.Combine (BUNDLE_ASSETS_PATH, manifest_file_name));
		fileInfo.MoveTo (Path.Combine(STREAMING_ASSETS_PATH, manifest_file_name ));
    }
}

