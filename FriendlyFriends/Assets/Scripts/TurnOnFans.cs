using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnFans : MonoBehaviour
{
    public CeilingFan[] fans;



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //turn on all the fans
            foreach (CeilingFan f in fans)
            {
                f.SetSpinning(true);
            }
            //Get rid of the steam wall blocking further progress
            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);
        }

    }
}
