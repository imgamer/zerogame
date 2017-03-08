﻿using UnityEditor;
using UnityEngine;

using System.IO;
using System.Text;
using System.Collections.Generic;

public abstract class Packer 
{
    public static readonly string STREAMING_ASSETS_PATH = "Assets/StreamingAssets";
    public static readonly string BUNDLE_ASSETS_PATH = "Assets/Game/GameAssets/BundleAssets";
    public static readonly string SERVER_ASSETS_PATH = "Assets/Game/GameAssets/ServerAssets";

    // 这里的资源在Bundle方式打包时会生成bundle包，打包前会把其中的Resources目录改名为Packages目录
	public static readonly string ASSETS_PATH = "Assets/Game/GameAssets";
	public static readonly string UNFIXED_ASSETS_PATH = ASSETS_PATH + "/UnfixedAssets";
	public static readonly string FIXED_ASSETS_PATH = ASSETS_PATH + "/FixedAssets";
	public static readonly string ASSETS_CONFIG_FILE_PATH = string.Format("{0}/{1}", ASSETS_PATH, "assets_table.txt");

	// scene需要单独打包
	public static readonly string SCENE_FILE_PATH = "Assets/Game/GameAssets/UnfixedAssets/Scenes";

    public static readonly string RESOURCES_DIR_NAME = "Resources";
    public static readonly string PACKAGES_DIR_NAME = "Packages";

	public static readonly string ASSETS_CONFIG_FILE_NAME = "assets_table";
	public static readonly string BUNDLE_FILE_NAME = "BundleAssets";   // 此文件由打包功能生成，命名和打包的目标路径目录相同

	protected Dictionary<string, AssetsDetail> m_assetsDetailDict = new Dictionary<string, AssetsDetail>();
    protected Dictionary<AssetsType, List<AssetBundleBuild>> m_buildAssetsBundleDict = new Dictionary<AssetsType, List<AssetBundleBuild>>();

    protected AssetsType m_assetsType = AssetsType.Resources;

    // 区分了不同打包资源的类型目前最大的作用是方便调试时查看数据
    protected Dictionary<AssetsType, List<AssetBundleBuild>> m_bundleBuildDict = new Dictionary<AssetsType, List<AssetBundleBuild>>();
	
	protected abstract List<string> GetAssetsPaths();
	public abstract void SetSencesInBuild();
	public abstract void PackAssets();
	public abstract void DistributeAssets();

    /// <summary>
    /// 把分发的打包文件收回打包目录
    /// </summary>
    public virtual void ResetAssets()
    {
        DirectoryInfo streamingAssetsDir = new DirectoryInfo( STREAMING_ASSETS_PATH );
        foreach( FileInfo file in streamingAssetsDir.GetFiles() )
        {
            file.MoveTo(Path.Combine(BUNDLE_ASSETS_PATH, file.Name));
        }

        DirectoryInfo serverAssetsDir = new DirectoryInfo(SERVER_ASSETS_PATH);
        foreach( FileInfo file in serverAssetsDir.GetFiles() )
        {
            file.MoveTo(Path.Combine(BUNDLE_ASSETS_PATH, file.Name));
        }

        AssetDatabase.Refresh();
    }

	protected void UpdateDetailDict()
	{
        foreach (string dirPath in GetAssetsPaths().ToArray())
        {
            List<string> filepaths = GetAssetsPathsByDir(dirPath);
            foreach (string filepath in filepaths)
            {
                UnityEngine.Object unityobj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(filepath);
                if (unityobj)
                {
                    string[] strs = filepath.Split(new char[] { '.' });
                    //string assetName = strs[0].Replace(filepath + "/", "");
                    if (string.IsNullOrEmpty(unityobj.name))
                    {
                        Debug.LogError("资源包名字为空。");
                        continue;
                    }
                    if (!unityobj.name.Equals(unityobj.name.ToLower()))
                    {
                        Debug.LogError(string.Format("资源包({0})命名不规范，必须全部小写字母。资源路径：{1}.", unityobj.name, filepath));
                        continue;
                    }

					string relativePath = filepath.Replace(dirPath+"/", "");
					AssetsDetail assetsDetail = new AssetsDetail(unityobj.name, m_assetsType, relativePath, 0, 0);
					m_assetsDetailDict.Add(unityobj.name, assetsDetail);
                }
            }
        }
	}

	protected List<string> GetAssetsPathsByDir( string p_dirpath )
	{
		List<string> paths = new List<string> ();
		if (string.IsNullOrEmpty (p_dirpath))
			return paths;

        string[] assetsPaths = Directory.GetFiles(p_dirpath, "*.*", SearchOption.AllDirectories);
		foreach (string filepath in assetsPaths) 
		{
			string str = filepath.Replace( '\\', '/' );
			paths.Add( str );
		}

		return paths;
	}

    protected void CreateConfigTableFile()
    {
        UpdateDetailDict();

        FileStream fs = File.Open(ASSETS_CONFIG_FILE_PATH, FileMode.OpenOrCreate, FileAccess.Write);
        fs.Seek(0, SeekOrigin.Begin );
        fs.SetLength(0);
        fs.Close();

        StreamWriter strw = new StreamWriter(ASSETS_CONFIG_FILE_PATH, true, Encoding.UTF8);
        foreach( AssetsDetail item in m_assetsDetailDict.Values )
        {
            strw.WriteLine(item.name + "\t" + item.type + "\t" + item.path + "\t" + item.crc + "\t" + item.size + "\n");
        }
        strw.Flush();
        strw.Close();
        AssetDatabase.Refresh();

    }

    protected void UpdateConfigTableBundleBuild()
    {
        List<AssetBundleBuild> bundleBuildList;
		m_buildAssetsBundleDict.TryGetValue(m_assetsType, out bundleBuildList);
        if(bundleBuildList == null)
        {
            bundleBuildList = new List<AssetBundleBuild>();
			m_buildAssetsBundleDict.Add(m_assetsType, bundleBuildList);
        }

        AssetBundleBuild bundleBuild = new AssetBundleBuild();
        bundleBuild.assetBundleName = ASSETS_CONFIG_FILE_NAME;
        bundleBuild.assetNames = new string[] { ASSETS_CONFIG_FILE_PATH };
        bundleBuildList.Add(bundleBuild);
    }

    protected void BuildAssetsBundle()
    {
        UpdateConfigTableBundleBuild();

        List<AssetBundleBuild> assetBundleBuildList = new List<AssetBundleBuild>();
        foreach( List<AssetBundleBuild> bundleBuilds in m_buildAssetsBundleDict.Values )
        {
            assetBundleBuildList.AddRange(bundleBuilds);
        }

		string bundleLocalPath = Application.dataPath.Replace("/Assets","/") + BUNDLE_ASSETS_PATH;
        BuildTarget buildTarget = GetBuildTarget();
        BuildPipeline.BuildAssetBundles(bundleLocalPath, assetBundleBuildList.ToArray(), BuildAssetBundleOptions.None, buildTarget );

        AssetDatabase.Refresh();
    }

    private static BuildTarget GetBuildTarget()
    {
        BuildTarget target = BuildTarget.StandaloneWindows;
#if UNITY_ANDROID
            target = BuildTarget.Android;
#elif UNITY_IPHONE
        target = BuildTarget.iPhone;
#elif UNITY_WEBPLAYER
        target = BuildTarget.WebPlayer;
#elif UNITY_STANDALONE_WIN
        target = BuildTarget.StandaloneWindows;
#elif UNITY_FLASH
        target = BuildTarget.FlashPlayer;
#endif
        return target;
    }
}
