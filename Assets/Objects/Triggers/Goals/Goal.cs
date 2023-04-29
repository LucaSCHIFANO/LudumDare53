using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour, ICallable
{
    [SerializeField] private GoalManagerRef gmref;
    [SerializeField] GameObject go;
    public void Activate()
    {
        go.SetActive(true);
    }

    public void Interacted()
    {
        if(gmref.Instance.IsGoalCompleted()) go.SetActive(false);
    }
}
