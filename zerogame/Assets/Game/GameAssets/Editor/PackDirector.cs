using UnityEngine;
using System.Collections;

public class PackDirector
{
	private Packer m_packer;

	public PackDirector( Packer p_packer )
	{
		m_packer = p_packer;
	}

	public void Pack()
	{
        m_packer.Prepare();
        m_packer.BuildAssetsBundle();
        m_packer.BuildBundleInfoFile();
		m_packer.DistributeAssets();
	}

}
