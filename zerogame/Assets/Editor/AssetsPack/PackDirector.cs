using UnityEngine;
using System.Collections;

public class PackDirector
{
	private Packer _packer;

	public PackDirector( Packer p_packer )
	{
		_packer = p_packer;
	}

	public void Pack()
	{
		_packer.SetSencesInBuild ();
		_packer.ResetAssets ();
		_packer.PackAssets ();
		_packer.DistributeAssets ();

	}

}
