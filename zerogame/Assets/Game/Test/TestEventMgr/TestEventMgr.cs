using UnityEngine;

using System;
using System.Collections;

namespace TestDelegate
{

class TestEventArg : EventArgs
{

}

public class TestEventMgr : Singleton<TestEventMgr>
{
	protected override void OnInit()
	{}
	
	protected override void OnFinish()
	{}

}



}   // end namesapce TestDelegate