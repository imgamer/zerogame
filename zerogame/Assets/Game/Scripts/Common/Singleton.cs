using UnityEngine;
using System.Collections;

public abstract class Singleton<T> where T : Singleton<T>, new()
{
    private static T _instance = null;

    protected Singleton()
    {
        SFDebug.Log("Singleton({0}) init....", this.ToString());
    }

    public static void Create()
    {
        if( _instance == null )
        {
            _instance = new T();
        }
    }

    public static T Instance
    {
        get
        {
            if( _instance == null )
            {
                Create();
            }
            return _instance;
        }
    }

}
