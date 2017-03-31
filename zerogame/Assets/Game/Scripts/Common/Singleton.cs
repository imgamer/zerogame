using UnityEngine;
using System;
using System.Collections;

public abstract class Singleton<T> where T : Singleton<T>, new()
{
    private static T m_instance = null;

    public Singleton()
    {
        Debug.Assert(m_instance == null);
        m_instance = (T)this;
        Logger.Log("Singleton({0}) init....", this.ToString());
    }

	~Singleton()
	{
		m_instance.Finish();
	}

    public static void Create()
    {
        if( m_instance == null )
        {
            m_instance = new T();
			m_instance.Init();
        }
    }

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

	private void Init()
	{
		OnInit ();
	}

	private void Finish()
	{
		OnFinish ();
	}

	abstract protected void OnInit();
	abstract protected void OnFinish();
}
