using UnityEngine;
using System.Collections.Generic;

public class AssetsLoader : MonoSingleton<AssetsLoader>
{
    private Queue<LoadTask> m_taskQueue = new Queue<LoadTask>();

    public void Load(AssetsRequest p_request)
    {
    }

    protected override void OnInit()
    {
    }

    protected override void OnFinish()
    {
    }

}
