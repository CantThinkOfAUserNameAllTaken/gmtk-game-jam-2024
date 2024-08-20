using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRunTimeSet<T> : ScriptableObject
{
    private List<T> _runTimeList;

    public void Register(T t)
    {
        _runTimeList.Add(t);
    }

    public void UnRegister(T t)
    {
        _runTimeList.Remove(t);
    }

    public List<T> GetList()
    {
        return _runTimeList;
    }

    public T GetItemAtIndex(int index)
    {
        return _runTimeList[index];
    }
}
