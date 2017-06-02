using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public delegate void Event_Callback<T>(T p_arg);

public static class EventManager
{
    private static Dictionary<EventID, Delegate> m_events = new Dictionary<EventID, Delegate>();

    public static void Register<T>(EventID p_eventID, Event_Callback<T> p_callback)
    {
        lock(m_events)
        {
            if (!m_events.ContainsKey(p_eventID))
                m_events.Add(p_eventID, p_callback);
            else
                m_events[p_eventID] = (Event_Callback<T>)m_events[p_eventID] + p_callback;
        }
    }

    public static void Deregister<T>(EventID p_eventID, Event_Callback<T> p_callback)
    {
        lock(m_events)
        {
            if(m_events.ContainsKey(p_eventID))
            {
                m_events[p_eventID] = (Event_Callback<T>)m_events[p_eventID] - p_callback;
                if(m_events[p_eventID] == null)
                {
                    m_events.Remove(p_eventID);
                }
            }
        }
    }

    public static void Fire<T>(EventID p_eventID, T p_arg)
    {
        Delegate dlg = null;
        if(m_events.TryGetValue(p_eventID, out dlg))
        {
            Event_Callback<T> callback = (Event_Callback<T>)dlg;    // dlg只是变量，不能直接转为方法调用，需要赋值给原生定义方法。
            try
            {
                callback(p_arg);
            }
            catch(Exception e)
            {
                Debug.LogError("EventManager::Fire:" + p_eventID.ToString() + "\n" + e.ToString());
            }
        }
    }

    public static void ClearEvents()
    {
        m_events.Clear();
    }
}
