using UnityEngine;
using System.Collections;

// 资源类型
public enum AssetsType
{
    Resources,      // 可直接从Resources读取的prefab
    Local,          // 需从StreamingAssets读取bundle包
    Server          // 需要从服务端读取，可能下载后会放置于ServerAssets目录
}

public struct AssetsDetail
{
	public string name;
    public AssetsType type;
    public string path;
    public uint crc;			// 文件crc，写到配置后可以用来判断资源是否过时了
    public int size;			// 

	public AssetsDetail( string p_name, AssetsType p_type, string p_path, uint p_crc, int p_size )
	{
		name = p_name;
		type = p_type;
		path = p_path;
		crc = p_crc;
		size = p_size;
	}
}
