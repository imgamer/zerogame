using UnityEngine;
using System.Collections;


public class TestMgr : Singleton<TestMgr> 
{

	protected override void OnInit()
	{
		SingletonTest.Create();
		TestDelegate.TestEventMgr.Create();
	}
	
	protected override void OnFinish()
	{}
}
