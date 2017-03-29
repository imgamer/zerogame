using UnityEditor;
using UnityEngine;
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

    protected void UpdateSingleBundleBuild(string p_dirPath)
    {
        string dirPath = string.Format("{0}/{1}/{2}", UNFIXED_ASSETS_PATH, PACKAGES_DIR_NAME, p_dirPath);

        List<AssetBundleBuild> bundleBuildList = new List<AssetBundleBuild>();
        int bundleBuildCount = 0;
        string assetBundleVariant = GetBundleVariant();
        foreach( string assetPath in GetPaths(dirPath) )
        {
            UnityEngine.Object uobject = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));
            if(uobject != null)
            {
                AssetBundleBuild bundleBuild = new AssetBundleBuild();
                bundleBuild.assetBundleName = uobject.name; // 文件名作为bundle包名，约定为资源文件名必须全部小写
                bundleBuild.assetBundleVariant = assetBundleVariant;
                bundleBuild.assetNames = new string[]{assetPath};
                bundleBuildList.Add(bundleBuild);
                ++bundleBuildCount;
            }
        }
        if(bundleBuildCount == 0)
        {
            Debug.LogWarning(string.Format("BundlePacker::UpdateSingleBundleBuild:{0}没有资源用于打包bundle。", p_dirPath));
            return;
        }

        Debug.LogWarning(string.Format("BundlePacker::UpdateSingleBundleBuild:bundle数量{0}。", p_dirPath));
        List<AssetBundleBuild> alreadyBundleBuild;
        if (!m_buildAssetsBundleDict.TryGetValue(m_assetsType, out alreadyBundleBuild))
        {
            m_buildAssetsBundleDict[m_assetsType] = bundleBuildList;
        }
        else
        {
            alreadyBundleBuild.AddRange(bundleBuildList);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p_dirName">第一层目录名</param>
    protected void UpdateGroupBundleBuild(string p_dirName)
    {
        string dirPath = string.Format("{0}/{1}/{2}", UNFIXED_ASSETS_PATH, PACKAGES_DIR_NAME, p_dirName);

        List<string> groupPaths = new List<string>();
        foreach(string assetPath in GetPaths(dirPath))
        {
            UnityEngine.Object uobject = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));
            if (uobject != null)
                groupPaths.Add(assetPath);
        }

        if(groupPaths.Count == 0)
        {
            Debug.LogWarning(string.Format("BundlePacker::UpdateGroupBundleBuild:{0} has nothing to pack.", p_dirName));
            return;
        }

        List<AssetBundleBuild> alreadyBundleBuilds;
        if( !m_buildAssetsBundleDict.TryGetValue(m_assetsType, out alreadyBundleBuilds) )
        {
            alreadyBundleBuilds = new List<AssetBundleBuild>();
            m_buildAssetsBundleDict[m_assetsType] = alreadyBundleBuilds;
        }

        AssetBundleBuild bundleBuild = new AssetBundleBuild();
        bundleBuild.assetBundleName = p_dirName.ToLower();
        bundleBuild.assetBundleVariant = GetBundleVariant();
        bundleBuild.assetNames = groupPaths.ToArray();
        alreadyBundleBuilds.Add(bundleBuild);
    }

    protected void UpdateShareBunldeBuild(string p_dirName)
    { }

    protected override void UpdateBundleBuild()
    {
        base.UpdateBundleBuild();
        // todo：打包资源目录需要改成可配置
        UpdateSingleBundleBuild("Models");
        UpdateGroupBundleBuild("Configs");
    }

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
