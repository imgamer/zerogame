using UnityEngine;

class Main: MonoBehaviour
{
    private static Main m_instance = null;

    public Main Instance
    {
        get
        {
            return Main.m_instance;
        }
    }

    void Awake()
    {
        Logger.Log("Game awake...");
        CheckSingleton();
    }

    void Start()
    {
        Logger.Log("Game start...");
        
        InitSingleton();

        MonoSingletonMgr.Create();

        CheckAssets();
    }

    private void CheckAssets()
    {
    }

    private void CheckSingleton()
    {
        if( m_instance == null)
        {
            m_instance = this;
            Logger.Log("Main::CheckSingleton...good..");
        }
        else if( m_instance != this )
        {
            GameObject.Destroy(this);
            Logger.LogWarning("There is already a instance of Main created( {0} ), now destoy the new one.", m_instance.name );
        }
    }

    private void InitSingleton()
    {
        // To create C# Singleton here.
    }
}