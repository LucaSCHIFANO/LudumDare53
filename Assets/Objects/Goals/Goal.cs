using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GoalManagerRef gmref;
    [SerializeField] GameObject go;
    public void Activate()
    {
        go.SetActive(true);
    }

    public void Deactivate() 
    {
        gmref.Instance.GoalCompleted();
        go.SetActive(false);
    }

    
}
