using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private GameObject position;

    [Header("Target")]
    [SerializeField] private GoalManagerRef gmRef;
    private Transform target;

    private void Start()
    {
        gmRef.Instance.SetArrow(this);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        transform.position = position.transform.position;
        if(target != null) transform.LookAt(target);
    }
}
