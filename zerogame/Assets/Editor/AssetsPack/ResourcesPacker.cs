using UnityEditor;
using System.Collections;

public class ResourcesPacker : Packer 
{
	
    public ResourcesPacker()
    {
        assetsType = AssetsType.Resources;
    }

    protected override void Initialize()
    {
        SFDebug.Log("ResourcesPacker::Initialize.");
        assetsList.Add(SCENE_FILE_PATH);
        assetsList.Add(string.Format("{0}/{1}", FIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
        assetsList.Add(string.Format( "{0}/{1}", UNFIXED_ASSETS_PATH, RESOURCES_DIR_NAME));
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

		_assetsDetailDict.Add (ASSETS_CONFIG_FILE_NAME, new AssetsDetail ( ASSETS_CONFIG_FILE_NAME, AssetsType.Resources, string.Empty, 0, 0 ));
		_assetsDetailDict.Add (BUNDLE_MANIFEST_FILE_NAME, new AssetsDetail ( BUNDLE_MANIFEST_FILE_NAME, AssetsType.Resources, string.Empty, 0, 0 ));

        UpdateDetailDict(assetsList.ToArray());
	}
	public override void DistributeAssets() {}
}

