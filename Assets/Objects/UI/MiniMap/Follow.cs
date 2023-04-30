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
        transform.position = position.transform.position;
    }
}
