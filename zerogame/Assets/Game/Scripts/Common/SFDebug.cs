using UnityEngine;
using System.Collections;


public static class SFDebug
{
    public static void Log( string p_formatstring, params object[] p_args )
    {
        Debug.Log("[DEBUG]:"+string.Format(p_formatstring, p_args));
    }

    public static void LogWarning(string p_formatstring, params object[] p_args)
    {
        Debug.LogWarning("[WARNING]:"+string.Format(p_formatstring, p_args));
    }

    public static void LogError( string p_formatstring, params object[] p_args )
    {
        Debug.LogError("[ERROR]:" + string.Format(p_formatstring, p_args));
    }
}
