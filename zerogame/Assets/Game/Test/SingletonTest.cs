using UnityEngine;
using System.Collections;

public class SingletonTest : Singleton<SingletonTest> 
{
    public SingletonTest()
    {
        Logger.Log("SingletonTest::SingletonTest.....");
    }
}
