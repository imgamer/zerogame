using UnityEngine;
using System;
using System.Collections;


// 维护全局组件容器"Singleton"游戏对象
public class MonoSingletonMgr
{
	public static readonly string SINGLETON_GAMEOBJECT_NAME = "Singleton";

	private static MonoSingletonMgr m_instance;
	private static GameObject m_singleton;

	private MonoSingletonMgr()
	{
		SFDebug.Log("MonoSingletonMgr constructed...");
	}

	public static void Create()
	{
		if (m_instance == null) 
		{
			m_instance = new MonoSingletonMgr();

			GameObject singleton = GameObject.Find(SINGLETON_GAMEOBJECT_NAME);
			if(singleton == null)
			{
				singleton = new GameObject(SINGLETON_GAMEOBJECT_NAME);
                MonoSingletonMgr.m_singleton = singleton;
				GameObject.DontDestroyOnLoad(singleton);
			}

            m_instance.InitSingleton();
		}
	}

	public static MonoSingletonMgr Instance
	{
		get
		{
			if(m_instance == null)
			{
				Create();
			}

			return MonoSingletonMgr.m_instance;
		}
	}

    public T AddSingletonComponent<T>() where T: Component
    {
        T t = m_singleton.GetComponent<T>();
        if( t == null )
        {
            t = m_singleton.AddComponent<T>();
        }
        return t;
    }

	private void InitSingleton()
	{
	}
}
