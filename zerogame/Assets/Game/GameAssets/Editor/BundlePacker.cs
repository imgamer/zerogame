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
		assetsList.Add(string.Format("{0}/{1}", FIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
		assetsList.Add(string.Format( "{0}/{1}", UNFIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
		return assetsList;
	}

	public override void SetSencesInBuild()
    {
        EditorBuildSettings.scenes[0].enabled = true;
        for( int i = 1; i < EditorBuildSettings.scenes.Length; i++ )
        {
            EditorBuildSettings.scenes[i].enabled = false;
        }
    }

	public override void PackAssets() 
	{
		AssetDatabase.RenameAsset(string.Format("{0}/{1}", UNFIXED_ASSETS_PATH, PACKAGES_DIR_NAME), PACKAGES_DIR_NAME);
		AssetDatabase.Refresh();
	}
	public override void DistributeAssets()
	{
		DirectoryInfo dir_info = new DirectoryInfo (BUNDLE_ASSETS_PATH);
		foreach(FileInfo file in dir_info.GetFiles())
		{
			file.MoveTo(Path.Combine(STREAMING_ASSETS_PATH, file.Name ) );
		}
	}
}
