using UnityEditor;

using System.IO;
using System.Collections.Generic;

public abstract class Packer 
{
    public static readonly string STREAMING_ASSETS_PATH = "Assets/StreamingAssets";
    public static readonly string BUNDLE_ASSETS_PATH = "Assets/Game/GameAssets/BundleAssets";
    public static readonly string SERVER_ASSETS_PATH = "Assets/Game/GameAssets/ServerAssets";

	// scene需要单独打包
	public static readonly string SCENE_FILE_PATH = "Assets/Game/GameAssets/UnfixedAssets/Scenes";

    // 这里的资源在Bundle方式打包时会生成bundle包，打包前会把其中的Resources目录改名为Packages目录
    public static readonly string UNFIXED_ASSETS_PATH = "Assets/Game/GameAssets/UnfixedAssets";

	public static readonly string FIXED_ASSETS_PATH = "Assets/Game/GameAssets/FixedAssets";

    public static readonly string RESOURCES_DIR_NAME = "Resources";
    public static readonly string PACKAGES_DIR_NAME = "Packages";

	public static readonly string ASSETS_CONFIG_FILE_NAME = "assets_table";
	public static readonly string BUNDLE_MANIFEST_FILE_NAME = "BundleAssets";

	protected Dictionary<string, AssetsDetail> _assetsDetailDict = new Dictionary<string, AssetsDetail>();

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

	//protected void updateDetailDict( s)
}
