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
		assetsList.Add(Path.Combine(FIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
		assetsList.Add(Path.Combine(UNFIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
		return assetsList;
	}

    protected override void UpdateBundleBuild()
    {}

	protected override void SetSencesInBuild() 
    {
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++ )
        {
            EditorBuildSettings.scenes[i].enabled = true;
        }
    }

    protected override void RenameResourceDir()
    {
        AssetDatabase.RenameAsset(Path.Combine(UNFIXED_ASSETS_PATH, PACKAGES_DIR_NAME), RESOURCES_DIR_NAME);
        AssetDatabase.Refresh();
    }

	public override void DistributeAssets() 
    {
        MoveFile2StreamingDir(ASSETS_CONFIG_FILE_NAME); // assets_table
        MoveFile2StreamingDir(BUNDLE_FILE_NAME);        // BundleAssets
        
    }

    private void MoveFile2StreamingDir(string p_fileName)
    {
        FileInfo fileInfo = new FileInfo(Path.Combine(BUNDLE_ASSETS_PATH, p_fileName));
        fileInfo.MoveTo(Path.Combine(STREAMING_ASSETS_PATH, p_fileName));
        string manifest_file_name = p_fileName + ".manifest";
        fileInfo = new FileInfo(Path.Combine(BUNDLE_ASSETS_PATH, manifest_file_name));
        fileInfo.MoveTo(Path.Combine(STREAMING_ASSETS_PATH, manifest_file_name));
		AssetDatabase.Refresh();
    }
}

