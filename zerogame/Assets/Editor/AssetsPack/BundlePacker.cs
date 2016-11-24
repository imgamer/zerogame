﻿using UnityEditor;
using System.Collections;

public class BundlePacker : Packer 
{

	public override void SetSencesInBuild()
    {
        EditorBuildSettings.scenes[0].enabled = true;
        for( int i = 1; i < EditorBuildSettings.scenes.Length; i++ )
        {
            EditorBuildSettings.scenes[i].enabled = false;
        }
    }

	public override void PackAssets() {}
	public override void DistributeAssets() {}
}