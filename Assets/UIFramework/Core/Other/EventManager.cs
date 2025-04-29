using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

    private Dictionary<string, Delegate> genericEventDictionary = new Dictionary<string, Delegate>();

    public void RegisterEvent(string eventName, Action action)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = null;
        }
        eventDictionary[eventName] += action;
    }

    public void RegisterEvent<T>(string eventName, Action<T> action)
    {
        if (!genericEventDictionary.ContainsKey(eventName))
        {
            genericEventDictionary[eventName] = null;
        }
        var existingAction = (Action<T>)genericEventDictionary[eventName];
        genericEventDictionary[eventName] = existingAction + action;
    }

    public void UnregisterEvent(string eventName, Action action)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= action;
            if (eventDictionary[eventName] == null)
            {
                eventDictionary.Remove(eventName);
            }
        }
    }

    public void UnregisterEvent<T>(string eventName, Action<T> action)
    {
        if (genericEventDictionary.ContainsKey(eventName))
        {
            var existingAction = (Action<T>)genericEventDictionary[eventName];
            genericEventDictionary[eventName] = existingAction - action;
            if (genericEventDictionary[eventName] == null)
            {
                genericEventDictionary.Remove(eventName);
            }
        }
    }

    public void TriggerEvent(string eventName)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName]?.Invoke();
        }
    }

    public void BroadcastEvent<T>(string eventName, T parameter)
    {
        if (genericEventDictionary.ContainsKey(eventName))
        {
            var action = (Action<T>)genericEventDictionary[eventName];
            action?.Invoke(parameter);
        }
    }
}
