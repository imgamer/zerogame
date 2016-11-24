using UnityEngine;
using System.Collections;

// 资源类型
public enum AssetsType
{
    Resources,      // 可直接从Resources读取的prefab
    Local,          // 需从StreamingAssets读取bundle包
    Server          // 需要从服务端读取，可能下载后会放置于ServerAssets目录
}


