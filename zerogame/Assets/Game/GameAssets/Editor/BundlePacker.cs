using UnityEditor;
using System.Collections.Generic;
using System.IO;


public class BundlePacker : Packer 
{
    public BundlePacker()
    {
        m_assetsType = AssetsType.Local;
    }

	protected override List<string> GetAssetsPaths()
	{
		List<string> assetsList = new List<string> ();
		assetsList.Add(SCENE_FILE_PATH);
        assetsList.Add(Path.Combine( UNFIXED_ASSETS_PATH, PACKAGES_DIR_NAME));
		return assetsList;
	}

    protected override void UpdateBundleBuild()
    {}

    protected override void RenameResourceDir()
    {
        AssetDatabase.RenameAsset(Path.Combine(UNFIXED_ASSETS_PATH, RESOURCES_DIR_NAME), PACKAGES_DIR_NAME);
        AssetDatabase.Refresh();
    }

	protected override void SetSencesInBuild()
    {
        EditorBuildSettings.scenes[0].enabled = true;
        for( int i = 1; i < EditorBuildSettings.scenes.Length; i++ )
        {
            EditorBuildSettings.scenes[i].enabled = false;
        }
    }

	public override void DistributeAssets()
	{
		DirectoryInfo dir_info = new DirectoryInfo (BUNDLE_ASSETS_PATH);
		foreach(FileInfo file in dir_info.GetFiles())
		{
			file.MoveTo(Path.Combine(STREAMING_ASSETS_PATH, file.Name ) );
		}

        AssetDatabase.Refresh();
	}
}
