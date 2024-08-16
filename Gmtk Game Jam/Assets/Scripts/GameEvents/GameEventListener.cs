using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// the Base class for all gameEventListeners
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class GameEventListener<T> : MonoBehaviour
{
    /// <summary>
    /// the event the listener will register itself to
    /// </summary>
    public GameEvent<T> Event;

    /// <summary>
    /// the responce to be triggered when the event is raised
    /// </summary>
    public UnityEvent<T> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }
    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }
    /// <summary>
    /// when the event is raised, triggeres the response
    /// </summary>
    /// <param name="paranToBePassedOn"></param>
    public void OnEventRaised(T paranToBePassedOn)
    {
        Response.Invoke(paranToBePassedOn);
    }
}
