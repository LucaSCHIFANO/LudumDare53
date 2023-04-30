using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference<T> : ScriptableObject // this is how a learn to create "Singleton", every "ref" is then stored in a scriptable object 
{
    T instance;

    public T Instance { get => instance; set => Set(value); }

    void Set(T value)
    {
        instance = value;
    }
}
