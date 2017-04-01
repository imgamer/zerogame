using UnityEngine;
using System.Collections.Generic;

struct LoadAssetInfo
{
	 
	public string m_assetName;
	public GameObject m_gameObject;
}

public class AssetsRequest
{
	private Dictionary<string, LoadAssetInfo> m_requestAssets = new Dictionary<string, LoadAssetInfo>();

    public void OnAssetsLoaded( string p_assetsName, GameObject p_gameObject )
    {
		m_requestAssets [p_assetsName].m_gameObject = p_gameObject;
		if (IsAllAssetsLoaded)
			;
	}

	private bool IsAllAssetsLoaded()
	{
		return true;
	}
}
