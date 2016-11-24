using UnityEditor;
using System.Collections;

public class ResourcesPacker : Packer 
{
	
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


    }
	public override void DistributeAssets() {}
}

