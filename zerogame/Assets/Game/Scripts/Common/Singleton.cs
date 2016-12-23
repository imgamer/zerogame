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
        SFDebug.Log("Singleton({0}) init....", this.ToString());
    }

    public static void Create()
    {
        if( m_instance == null )
        {
            m_instance = new T();
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

}
