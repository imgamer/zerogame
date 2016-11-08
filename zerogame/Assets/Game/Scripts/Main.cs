using UnityEngine;

class Main: MonoBehaviour
{
    private static Main _instance = null;

    public Main Instance
    {
        get
        {
            return Main._instance;
        }
    }

    void Awake()
    {
        CheckSingleton();
        SFDebug.Log("Game awake...");
    }

    void Start()
    {
        SFDebug.Log("Game start...");
    }

    private void CheckSingleton()
    {
        if( _instance == null)
        {
            _instance = this;
            SFDebug.Log("Main::CheckSingleton...good..");
        }
        else if( _instance != this )
        {
            GameObject.Destroy(this);
            SFDebug.Warning("There is already a instance of Main created( {0} ), now destoy the new one.", _instance.name );
        }
    }
}