using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the Base class for all Game Events
/// </summary>
/// <typeparam name="T"></typeparam>
[CreateAssetMenu(menuName = "ScriptableObjects/Events/GameEvent")]
public abstract class GameEvent<T> : ScriptableObject
{
    /// <summary>
    /// all of the registered listeners to this event
    /// </summary>
    private List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();
    /// <summary>
    /// calls the responces of all registered listeners, passing paramToBePassedOn to them
    /// </summary>
    /// <param name="paramToBePassedOn"></param>
    public void Raise(T paramToBePassedOn)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(paramToBePassedOn);
        }

    }
    /// <summary>
    /// add the listener to the list of listeners
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(GameEventListener<T> listener)
    {
        listeners.Add(listener);
    }
    /// <summary>
    /// remove listener from listeners
    /// </summary>
    /// <param name="listener"></param>
    public void UnRegisterListener(GameEventListener<T> listener)
    {
        listeners.Remove(listener);
    }

}
