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

    //Keypress events
    public UnityEvent SuccessfulFlap;
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
        WingsUp = new UnityEvent();
        WingsDown = new UnityEvent();
    }
	
	void Update () {
		if (Input.GetAxis("FlapUp") == 1)
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

        if (Input.GetAxis("FlapDown") == 1)
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
	}
    #endregion
}
