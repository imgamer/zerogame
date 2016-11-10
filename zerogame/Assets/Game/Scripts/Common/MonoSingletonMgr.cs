using UnityEngine;
using System;
using System.Collections;


// 维护全局组件容器"Singleton"游戏对象
public class MonoSingletonMgr
{
	public static readonly string SINGLETON_GAMEOBJECT_NAME = "Singleton";

	private static MonoSingletonMgr _instance;
	private static GameObject _singleton;

	private MonoSingletonMgr()
	{
		SFDebug.Log("MonoSingletonMgr constructed...");
	}

	public static void Create()
	{
		if (_instance == null) 
		{
			_instance = new MonoSingletonMgr();

			GameObject singleton = GameObject.Find(SINGLETON_GAMEOBJECT_NAME);
			if(singleton == null)
			{
				singleton = new GameObject(SINGLETON_GAMEOBJECT_NAME);
                MonoSingletonMgr._singleton = singleton;
				GameObject.DontDestroyOnLoad(singleton);
			}

            _instance.InitSingleton();
		}
	}

	public static MonoSingletonMgr Instance
	{
		get
		{
			if(_instance == null)
			{
				Create();
			}

			return MonoSingletonMgr._instance;
		}
	}

    public T AddSingletonComponent<T>() where T: Component
    {
        T t = _singleton.GetComponent<T>();
        if( t == null )
        {
            t = _singleton.AddComponent<T>();
        }
        return t;
    }

	private void InitSingleton()
	{
	}
}
