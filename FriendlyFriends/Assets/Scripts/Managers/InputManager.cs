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
    public bool charging;

    //Keypress events
    public UnityEvent SuccessfulFlap;
    public UnityEvent BuildCharge;
    public UnityEvent ReleaseCharge;
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
        charging = false;

        //Initalize key press events here
        SuccessfulFlap = new UnityEvent();
        BuildCharge = new UnityEvent();
        ReleaseCharge = new UnityEvent();
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
                if (charging)
                    BuildCharge.Invoke();
                else
                    SuccessfulFlap.Invoke();
                

            } else
            {
                return;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.SetGameStateToPause();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
            charging = true;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            charging = false;
            ReleaseCharge.Invoke();
        }
            

	}
    #endregion
}
