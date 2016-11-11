using UnityEngine;
using System.Collections;

public class TestMain : MonoBehaviour {

    private static TestMain _instance = null;

    public TestMain Instance
    {
        get
        {
            return TestMain._instance;
        }
    }

    void Awake()
    {
        CheckSingleton();
        SFDebug.Log("Test game awake...");
    }

    void Start()
    {
        MonoSingletonMgr.Create();
        SingletonTest.Create();
        SFDebug.Log("Test game start...");
    }

    private void CheckSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
            SFDebug.Log("TestMain::CheckSingleton...good..");
        }
        else if (_instance != this)
        {
            GameObject.Destroy(this);
            SFDebug.LogWarning("There is already a instance of TestMain created( {0} ), now destoy the new one.", _instance.name);
        }
    }
}
