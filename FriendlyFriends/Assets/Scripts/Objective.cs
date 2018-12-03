using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    #region Variables
    private bool pressedSpace;
    #endregion

    #region Unity API Functions
    private void Start()
    {
        pressedSpace = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressedSpace = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pressedSpace = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        if (pressedSpace)
        {
            transform.SetParent(other.gameObject.transform);
        }
    }
    #endregion
}
