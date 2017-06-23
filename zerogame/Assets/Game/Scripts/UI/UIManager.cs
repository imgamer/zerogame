using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private static UIManager m_instance;
    public static UIManager Instance
    {
        get { return UIManager.m_instance; }
        private set { UIManager.m_instance = value; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.Init();
        }
        else if (Instance != this)
        {
            Debug.LogError("UIManager::Awake:Instance dumplicate.");
            Destroy(this);
        }
    }

    private void Init()
    { 
    }

    public void OpenUI(string p_uiPath)
    {
    }

    public void CloseUI(string p_uiPath)
    {
    }
}
