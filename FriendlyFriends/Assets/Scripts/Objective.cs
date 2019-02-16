using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    #region Variables
    private bool pressedSpace;
    private bool beingHeld;
    private Rigidbody rb;
    #endregion

    #region Unity API Functions
    private void Start()
    {
        pressedSpace = false;
        beingHeld = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Check if Player is currently holding the object
        if (beingHeld)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("Clearing parent");
                transform.parent = null;
                beingHeld = false;
                rb.isKinematic = false;
            }
        }
        //Logic for item gathering key presses
        else if (Input.GetKeyDown(KeyCode.B))
        {
            pressedSpace = true;
        } else if (Input.GetKeyUp(KeyCode.B))
        {
            pressedSpace = false;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger entered!");
        if (pressedSpace && other.gameObject.tag == "Player" && !beingHeld)
        {
            Debug.Log("Object picked up!");
            transform.SetParent(other.gameObject.transform);
            beingHeld = true;
            rb.isKinematic = true;
            pressedSpace = false;
        }
    }
    #endregion
}
