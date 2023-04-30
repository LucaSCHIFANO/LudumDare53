using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private GoalManagerRef _ref;
    [SerializeField] private PizzaManagerRef pmRef;
    [SerializeField] private TimerScoreRef tsRef;

    private List<Goal> goalList = new List<Goal>();
    private Goal lastGoal = null;
    private Arrow arrow;


    private void Awake()
    {
        _ref.Instance = this;

        for (int i = 0; i < transform.childCount; i++) // get every child (yellow areas) and store them
        {
            goalList.Add(transform.GetChild(i).GetComponent<Goal>());
        }

        SetNextGoal();
    }

    public void SetNextGoal()
    {
        bool isOkay = false;

        while (!isOkay) // take a yellow area and active it except if it is the same as the last one
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

        ChangeArrowTarget();
    }

    public bool IsGoalCompleted() // check if the player has a pizza
    {
        if (pmRef.Instance.IsPizzaGet)
        {
            GoalCompleted();
            return true;
        }
        else return false;
    }

    public void GoalCompleted() // the player has a pizza :D
    {
        pmRef.Instance.PizzaDelivered();
        SetNextGoal();
        tsRef.Instance.Scored();
    }

    public void SetArrow(Arrow _arrow)
    {
        arrow = _arrow;
        ChangeArrowTarget();
    }

    private void ChangeArrowTarget() // change the target of the arrow (the direction it looking at)
    {
        if(arrow != null)
        {
            arrow.SetTarget(lastGoal.transform);
        }
    }
}
