using UnityEngine;
using System.Collections;

public class SingletonTest : Singleton<SingletonTest> 
{
    public SingletonTest()
    {
        SFDebug.Log("SingletonTest::SingletonTest.....");
    }
}
