using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWTime : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy;
    private void Awake()
    {
        Destroy(gameObject, timeBeforeDestroy); // just destroy the object after a delay D:
    }
}
