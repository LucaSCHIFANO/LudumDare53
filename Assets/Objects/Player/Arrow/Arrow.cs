using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private GoalManagerRef gmRef;
    private Transform target;

    private void Awake() // this is because the arrow is stocked with the player prefab
    {
        transform.parent = null;
    }

    private void Start() // set this arrow as the main (and only) arrow
    {
        gmRef.Instance.SetArrow(this);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(target != null) transform.LookAt(target); // look at my target !
    }
}
