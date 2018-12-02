using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static InputManager Instance;

    //public variables
    public bool wingsUp;
    public bool goingForwards;
    public bool goingBackwards;

    //Keypress events
    public UnityEvent SuccessfulFlap;
    public UnityEvent RightRotation;
    public UnityEvent LeftRotation;
    public UnityEvent WingsUp;
    public UnityEvent WingsDown;
    #endregion

    #region Unity API Functions
    void Awake () {
        //Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        wingsUp = false;
        goingForwards = false;
        goingBackwards = false;

        //Initalize key press events here
        SuccessfulFlap = new UnityEvent();
        RightRotation = new UnityEvent();
        LeftRotation = new UnityEvent();
        WingsUp = new UnityEvent();
        WingsDown = new UnityEvent();
    }
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (wingsUp == false)
            {
                wingsUp = true;
                WingsUp.Invoke();
            } else
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (wingsUp == true)
            {
                wingsUp = false;
                WingsDown.Invoke();
                SuccessfulFlap.Invoke();
            } else
            {
                return;
            }
        }

        //Rotation checks
        if (Input.GetKey(KeyCode.D))
        {
            RightRotation.Invoke();
        }
        if (Input.GetKey(KeyCode.A))
        {
            LeftRotation.Invoke();
        }

        //Forward and backwards movement checks
        if (Input.GetKeyDown(KeyCode.W))
        {
            goingForwards = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            goingForwards = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            goingBackwards = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            goingBackwards = false;
        }
	}
    #endregion
}
