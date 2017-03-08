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
        m_packer.PreparePack();
		m_packer.PackAssets();
		m_packer.DistributeAssets();
	}

}
