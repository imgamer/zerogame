using UnityEngine;
using System.Collections;

public enum PoolName
{
    Model
}

public enum PoolType
{
    Default,    // 不进资源池，不创建不回收
    Once,       // 一次性资源池，只创建不回收
    Loop        // 循环型资源池，既创建也回收
}
