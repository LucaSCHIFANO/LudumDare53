using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private GameObject position;

    private void Awake()
    {
        transform.parent = null;
    }

    void Update()
    {
        transform.position = position.transform.position; // make an object follow his target (follow = has the same position)
    }
}
