using UnityEngine;
using System;
using System.Collections;



public class MonoSingletonMgr
{
	static readonly string SINGLETON_NAME = "Singleton";

	private static MonoSingletonMgr _instance;
	private static GameObject _singleton;

	private MonoSingletonMgr()
	{
		SFDebug.Log("MonoSingletonMgr construction...");
	}

	public static void Create()
	{
		if (_instance == null) 
		{
			_instance = new MonoSingletonMgr();

			GameObject singleton = GameObject.Find(SINGLETON_NAME);
			if(singleton == null)
			{
				singleton = new GameObject(SINGLETON_NAME);
				_instance._singleton = singleton;
				Object.DontDestroyOnLoad(singleton);
			}

			InitSingleton();
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

	private void InitSingleton()
	{
	}
}
