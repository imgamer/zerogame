using UnityEditor;
using System.Collections;

public class BundlePacker : Packer 
{
    public BundlePacker()
    {
        m_assetsType = AssetsType.Local;
    }

    protected override void Initialize()
    {
        Logger.Log("BundlePacker::Initialize.");
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
	public override void DistributeAssets() {}
}
