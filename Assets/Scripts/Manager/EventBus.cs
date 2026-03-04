using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus 
{
    private static Dictionary<string, List<Action<object>>> bus = new Dictionary<string, List<Action<object>>>(); //Observer pattern

    public static void AddListener(string eventName, Action<object> listener)
    {
        //Neu chua co event Name nay thi them vao.
        if (!bus.ContainsKey(eventName))
        {
            bus.Add(eventName, new List<Action<object>>());
        }

            bus[eventName].Add(listener); 
    }
    public static void RemoveListener(string eventName, Action<object> listener)
    {
        if (!bus.ContainsKey(eventName))
        {
            return;
        }
            bus[eventName].Remove(listener);
    }
    public static void ClearAll() //Clear het de tranh bi memory leak
    {
        bus.Clear();
    }
    public static void Notify(string eventName, object data)
    {
        if (!bus.ContainsKey(eventName))
        {
            return;
        }
        foreach (var ev in bus[eventName])
        {
            try
            {
                ev?.Invoke(data);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error invoking event {eventName}: {e}");
            }
        }
    }
}
