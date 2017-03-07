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
        CheckSingleton();
        Logger.Log("Game awake...");
    }

    void Start()
    {
        MonoSingletonMgr.Create();

        // To create C# Singleton here.

        Logger.Log("Game start...");
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
}