using UnityEditor;
using System.Collections;
using System.IO;


public class ResourcesPacker : Packer 
{
	
    public ResourcesPacker()
    {
        m_assetsType = AssetsType.Resources;
    }

    protected override void Initialize()
    {
        SFDebug.Log("ResourcesPacker::Initialize.");
        m_assetsList.Add(SCENE_FILE_PATH);
        m_assetsList.Add(string.Format("{0}/{1}", FIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
        m_assetsList.Add(string.Format( "{0}/{1}", UNFIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
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
		m_assetsDetailDict.Add (BUNDLE_MANIFEST_FILE_NAME, new AssetsDetail ( BUNDLE_MANIFEST_FILE_NAME, AssetsType.Resources, string.Empty, 0, 0 ));

        UpdateDetailDict(m_assetsList.ToArray());
        CreateConfigTableFile();
        UpdateConfigTableBundleBuild();

        BuildAssetsBundle();
	}

	public override void DistributeAssets() 
    {
        SFDebug.Log( "ResourcesType DistributeAssets." );
		DirectoryInfo dir_info = new DirectoryInfo (BUNDLE_ASSETS_PATH);
		foreach(FileInfo file in dir_info.GetFiles())
		{
			file.MoveTo(Path.Combine(STREAMING_ASSETS_PATH, file.Name ) );
		}
    }
}

