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

	public override void ResetAssets() {}
	public override void PackAssets() {}
	public override void DistributeAssets() {}
}

