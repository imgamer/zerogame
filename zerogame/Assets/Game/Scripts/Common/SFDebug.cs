using UnityEngine;
using System.Collections;


public static class SFDebug
{
    static void Log( string p_formatstring, params object[] p_args )
    {
        Debug.Log(string.Format(p_formatstring, p_args));
    }

    static void Warning(string p_formatstring, params object[] p_args)
    {
        Debug.LogWarning(string.Format(p_formatstring, p_args));
    }

    static void Error( string p_formatstring, params object[] p_args )
    {
        Debug.LogError(string.Format(p_formatstring, p_args));
    }
}
