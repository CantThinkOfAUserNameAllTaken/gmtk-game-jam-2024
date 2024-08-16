using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRunTimeSet<T> : ScriptableObject
{
    private List<T> _runTimeList;

    void Register(T t)
    {
        _runTimeList.Add(t);
    }

    void UnRegister(T t)
    {
        _runTimeList.Remove(t);
    }

    List<T> GetList()
    {
        return _runTimeList;
    }

    T GetItemAtIndex(int index)
    {
        return _runTimeList[index];
    }
}
