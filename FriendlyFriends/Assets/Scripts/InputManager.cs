using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static InputManager Instance;

    //private variables
    public bool wingsUp;

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

        //Initalize key press events here
        SuccessfulFlap = new UnityEvent();
        RightRotation = new UnityEvent();
        LeftRotation = new UnityEvent();
        WingsUp = new UnityEvent();
        WingsDown = new UnityEvent();
    }
	
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow))
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

        if (Input.GetKey(KeyCode.DownArrow))
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
	}
    #endregion
}
