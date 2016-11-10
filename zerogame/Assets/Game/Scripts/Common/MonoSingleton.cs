using UnityEngine;
using System.Collections;

// 1. 组件创建策略
// 2. singleton模式实现
// 3. 初始化和反初始化流程
public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T> {

    private static T _instance = null;
    //private bool _inited = false;

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

    public static void Create()
    {
        if( _instance == null )
        {
            _instance = MonoSingletonMgr.Instance.AddSingletonComponent<T>();   // Awake执行
            _instance.Init();   // Init先于Start执行
        }
    }

    private void Init()
    {
        OnInit();
    }

    private void Finit()
    {
        OnFinit();
    }

    public static void Destroy()
    {
        if (_instance == null) return;
        _instance.Finit();
    }

    protected abstract void OnInit();
    protected abstract void OnFinit();
}
