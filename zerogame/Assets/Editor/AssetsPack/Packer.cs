
using System.Collections;

public abstract class Packer 
{
	public abstract void SetSencesInBuild();
	public abstract void ResetAssets();
	public abstract void PackAssets();
	public abstract void DistributeAssets();
}
