using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour, ICallable
{
    [SerializeField] private GoalManagerRef gmref;
    [SerializeField] GameObject go;
    public void Activate() // activate the yellow area
    {
        go.SetActive(true);
    }

    public void Interacted() // check if the player has a pizza, if he has one then he completed a delivery
    {
        if(gmref.Instance.IsGoalCompleted()) go.SetActive(false);
    }
}
