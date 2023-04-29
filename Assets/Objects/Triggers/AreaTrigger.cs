using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {
            transform.parent.GetComponent<ICallable>().Interacted();
        }
    }
}
