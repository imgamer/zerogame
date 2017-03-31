using UnityEngine;
using System.Collections;

public class SingletonTest : Singleton<SingletonTest> 
{
    public SingletonTest()
    {
        Logger.Log("SingletonTest::SingletonTest.....");
    }

	protected override void OnInit()
	{}

	protected override void OnFinish()
	{}
}
