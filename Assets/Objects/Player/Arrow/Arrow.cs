using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private GoalManagerRef gmRef;
    private Transform target;

    private void Awake()
    {
        transform.parent = null;
    }

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
        if(target != null) transform.LookAt(target);
    }
}
