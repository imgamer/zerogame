using UnityEngine;
using System.Collections;

public class GamePoolMgr : MonoBehaviour {

    public static GamePoolMgr Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public GameObject Spawn( string p_gameObjectName, PoolName p_poolName, PoolType p_poolType )
    {
        return null;
    }

    public void DeSpawn( GameObject p_gameobject )
    {
    }
}
