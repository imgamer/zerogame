using UnityEditor;
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
    public static readonly string BUNDLE_INFO_PATH = ASSETS_PATH + "/BundleInfo";
    public static readonly string ASSETS_CONFIG_FILE_PATH = string.Format("{0}/{1}", BUNDLE_INFO_PATH, "bundle_infos.txt");

	// scene需要单独打包
	public static readonly string SCENE_FILE_PATH = "Assets/Game/GameAssets/UnfixedAssets/Scenes";

    public static readonly string RESOURCES_DIR_NAME = "Resources";
    public static readonly string PACKAGES_DIR_NAME = "Packages";

    public static readonly string ASSETS_CONFIG_FILE_NAME = "bundle_infos";
	public static readonly string BUNDLE_FILE_NAME = "BundleAssets";   // 此文件由打包功能生成，与打包的输出目录同名

	protected Dictionary<string, AssetsDetail> m_assetsDetailDict = new Dictionary<string, AssetsDetail>();

    protected AssetsType m_assetsType = AssetsType.Resources;

    protected List<AssetBundleBuild> m_bundleBuildList = new List<AssetBundleBuild>();

	public void Prepare()
    {
        SetSencesInBuild();
        ResetAssets();
        RenameResourceDir();
    }
    
	public abstract void DistributeAssets();

    protected abstract void SetSencesInBuild();

    protected abstract void RenameResourceDir();

    /// <summary>
    /// 把分发的打包文件收回打包目录
    /// </summary>
    protected virtual void ResetAssets()
    {
        DirectoryInfo streamingAssetsDir = new DirectoryInfo( STREAMING_ASSETS_PATH );
        foreach( FileInfo file in streamingAssetsDir.GetFiles() )
        {
            if (file.Name.Split(new char[] { '.' })[0] == ASSETS_CONFIG_FILE_NAME)
            {
                file.MoveTo(Path.Combine(BUNDLE_INFO_PATH, file.Name));
                continue;
            }

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
        // 2个固有bundle包的信息
        m_assetsDetailDict.Add(ASSETS_CONFIG_FILE_NAME, new AssetsDetail(ASSETS_CONFIG_FILE_NAME, m_assetsType, string.Empty, 0, 0));
        m_assetsDetailDict.Add(BUNDLE_FILE_NAME, new AssetsDetail(BUNDLE_FILE_NAME, m_assetsType, string.Empty, 0, 0));

        // Resources资源信息
        foreach (string dirPath in GetResourcesAssetsPaths().ToArray())
        {
            List<string> filepaths = GetAssetsPathsByDir(dirPath);
            foreach (string filepath in filepaths)
            {
                UnityEngine.Object unityobj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(filepath);
                if (unityobj)
                {
                    //string[] strs = filepath.Split(new char[] { '.' });
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

        // Bundle包资源信息
        foreach(AssetBundleBuild bundleBuild in m_bundleBuildList)
        {
            string name = bundleBuild.assetBundleName;  // 名字必须全部小写

            string nameAndVariant = string.Format("{0}.{1}", bundleBuild.assetBundleName, bundleBuild.assetBundleVariant);
            string path = string.Format(string.Format("{0}/{1}", BUNDLE_ASSETS_PATH, nameAndVariant));
            uint crc;
            BuildPipeline.GetCRCForAssetBundle(path, out crc);
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            AssetsDetail detail = new AssetsDetail(name, m_assetsType, nameAndVariant, crc, (int)file.Length);
            file.Close();
            m_assetsDetailDict.Add(name, detail);
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

    protected void CreateBundleInfoFile()
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

    protected abstract List<string> GetResourcesAssetsPaths();

    protected abstract List<string> GetBundleAssetsPaths();
	
    public virtual void BuildAssetsBundle()
    {
        CollectBundleBuildInfo();   // 收集信息到m_bundleBuildList

        string bundleLocalPath = Application.dataPath.Replace("/Assets", "/") + BUNDLE_ASSETS_PATH;
        BuildTarget buildTarget = GetBuildTarget();
        BuildPipeline.BuildAssetBundles(bundleLocalPath, m_bundleBuildList.ToArray(), BuildAssetBundleOptions.None, buildTarget);

        AssetDatabase.Refresh();
    }

    public virtual void BuildBundleInfoFile()
    {
        CreateBundleInfoFile();

        string bundleLocalPath = Application.dataPath.Replace("/Assets", "/") + BUNDLE_INFO_PATH;
        BuildTarget buildTarget = GetBuildTarget();
        AssetBundleBuild bundleBuild = new AssetBundleBuild();
        bundleBuild.assetBundleName = ASSETS_CONFIG_FILE_NAME;
        bundleBuild.assetNames = new string[] { ASSETS_CONFIG_FILE_PATH };
        BuildPipeline.BuildAssetBundles(bundleLocalPath, new AssetBundleBuild[] { bundleBuild }, BuildAssetBundleOptions.None, buildTarget);

        AssetDatabase.Refresh();
    }

    protected virtual void CollectBundleBuildInfo()
    {
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

    protected static List<string> GetPaths(string path)
    {
        List<string> ret = new List<string>();

        if (string.IsNullOrEmpty(path)) return ret;
        string[] paths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        foreach (var p in paths)
        {
            string str = p.Replace('\\', '/');
            ret.Add(str);
        }
        return ret;
    }

    protected static string GetBundleVariant()
    {
        string variant = string.Format("s");
#if UNITY_ANDROID
            variant = string.Format("a");
#elif UNITY_IPHONE
        variant = string.Format("i");
#elif UNITY_WEBPLAYER
        variant = string.Format("w");
#elif UNITY_STANDALONE_WIN
        variant = string.Format("s");
#elif UNITY_FLASH
        variant = string.Format("f");
#endif
        return variant;
    }
}
