using UnityEngine;
using System.Collections.Generic;

struct LoadAssetInfo
{
	string m_assetName;
	GameObject m_gameObject;
}

public class AssetsRequest
{
	private Dictionary<string, LoadAssetInfo> m_requestAssets = new Dictionary<string, LoadAssetInfo>();

    public void OnAssetsLoaded( string p_assetsName )
    {}
}
