using UnityEngine;
using System;
using System.Collections;

// 维护全局组件容器"Singleton"游戏对象
public class MonoSingletonMgr: Singleton<MonoSingletonMgr>
{
	public static readonly string SINGLETON_GAMEOBJECT_NAME = "Singleton";

	private static GameObject m_singleton;

	public MonoSingletonMgr()
	{
		Logger.Log("MonoSingletonMgr constructed...");
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

	protected override void OnInit()
	{
		GameObject singleton = GameObject.Find(SINGLETON_GAMEOBJECT_NAME);
		if(singleton == null)
		{
			singleton = new GameObject(SINGLETON_GAMEOBJECT_NAME);
			MonoSingletonMgr.m_singleton = singleton;
			GameObject.DontDestroyOnLoad(singleton);
		}
		
		InitSingleton();
	}
	
	protected override void OnFinish()
	{}

	private void InitSingleton()
	{
	}
}
