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

    private GameObject GetUI(string p_uiPath)
    {
        return null;
    }

    public void OpenUI(string p_uiPath)
    {
        GameObject ui = GetUI(p_uiPath);
        if(ui == null)
        {
            Debug.LogError("UIManager::OpenUI:can open ui:" + p_uiPath);
            return;
        }

        ui.GetComponent<RectTransform>().SetParent(transform, false);
        UIWindow uiwin = ui.GetComponent<UIWindow>();
        uiwin.Init();
    }

    public void CloseUI(string p_uiPath)
    {
    }
}
