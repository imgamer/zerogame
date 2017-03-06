using UnityEngine;
using System.Collections;


public class TestMgr : Singleton<TestMgr> 
{
    public TestMgr()
    {
        Init();
    }

    private void Init()
    {
        SingletonTest.Create();
        TestDelegate.TestEventMgr.Create();
    }
}
