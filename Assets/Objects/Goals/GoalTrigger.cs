using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {
            transform.parent.GetComponent<Goal>().Deactivate();
        }
    }
}
