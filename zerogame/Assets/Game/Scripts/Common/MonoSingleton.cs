﻿using UnityEngine;
using System.Collections;

// 1. 组件创建策略
// 2. singleton模式实现
// 3. 初始化和反初始化流程
public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T> 
{
    private static T m_instance = null;
    //private bool _inited = false;

    public static T Instance
    {
        get
        {
            if( m_instance == null )
            {
                Create();
            }
            return m_instance;
        }
    }

    public static void Create()
    {
        if( m_instance == null )
        {
            m_instance = MonoSingletonMgr.Instance.AddSingletonComponent<T>();   // Awake执行
            m_instance.Init();   // Init先于Start执行
        }
    }

	public void OnDestroy()
	{
		Finish ();
	}

    private void Init()
    {
        OnInit();
    }

    private void Finish()
    {
        OnFinish();
    }

    public static void Destroy()
    {
        if (m_instance == null) return;
        m_instance.Finish();
    }

    protected abstract void OnInit();
    protected abstract void OnFinish();
}
