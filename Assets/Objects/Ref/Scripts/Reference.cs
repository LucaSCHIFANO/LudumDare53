using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference<T> : ScriptableObject
{
    T instance;

    public T Instance { get => instance; set => Set(value); }

    void Set(T value)
    {
        instance = value;
    }
}
