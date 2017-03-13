using UnityEngine;
using System.Collections;

public enum PublishMode
{
    ResourceMode,     //完整包模式
    LocalBundleMode,  //本地资源包模式
    ServerBundleMode, //网络资源包模式
}
public enum BuildMode
{
    Customer,    //用户模式
    Developer,   //开发者模式
}

/// <summary>
/// 依附于Main存在，仅用于配置全局参数
/// 和GlobalConfig同级的对象只能在Start中才能访问此对象实例
/// </summary>
public class GlobalConfig: MonoBehaviour 
{
    private static GlobalConfig m_instance;

    // 这里可以编辑器配置一些全局参数
    public PublishMode m_publishMode;

    // 增量更新服务器ip、端口、资源目录
    public string m_resouceServerIP = "127.0.0.1";
    public int m_resourceServerPort = 8080;
    public string m_resouceServerPath = "Assets";

    public bool m_localAssets = true;         // 是否从本地加载资源
    public bool m_versionVerify = false;      // 是否做资源版本验证以便做资源的增量更新

    private static bool m_inited = false;

    /// <summary>
    /// 现在只是临时的出错验证。
    /// TODO: 重构成singleton模式。
    /// </summary>
    public static GlobalConfig Instance
    {
        get
        {
            if (!m_inited)
            {
                Logger.LogError("GlobalConfig did not awake,please check!!必须作为Main的子对象存在！！");
                return null;
            }
            return m_instance;
        }
    }

    void Awake()
    {
        // Awake中启用，其它gameobject会在start时请求访问，不会导致错误。
        m_instance = this;
        m_inited = true;
    }

    public string GetAssetUrl()
    {
        return string.Format("{0}:{1}/{2}", m_resouceServerIP, m_resourceServerPort, m_resouceServerPath);
    }

    public bool IsLocalAssets()
    {
        return m_localAssets;
    }

    public bool IsVersionVerify()
    {
        return m_versionVerify;
    }
}
