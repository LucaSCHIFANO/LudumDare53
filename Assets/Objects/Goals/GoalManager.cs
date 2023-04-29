using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private GoalManagerRef _ref;
    private List<Goal> goalList = new List<Goal>();
    private Goal lastGoal = null;


    private void Awake()
    {
        _ref.Instance = this;

        for (int i = 0; i < transform.childCount; i++)
        {
            goalList.Add(transform.GetChild(i).GetComponent<Goal>());
        }

        SetNextGoal();
    }

    public void SetNextGoal()
    {
        bool isOkay = false;

        while (!isOkay)
        {
            int rnd = Random.Range(0, goalList.Count);
            
            for (int i = 0; i < goalList.Count; i++)
            {
                if (i == rnd && goalList[i] != lastGoal)
                {
                    goalList[i].Activate();
                    lastGoal = goalList[i];
                    isOkay = true;
                }
            }
        }
    }

    public void GoalCompleted()
    {
        SetNextGoal();
        Debug.Log("okay");
    }
}
