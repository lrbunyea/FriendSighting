using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static InputManager Instance;

    //Keypress events
    public UnityEvent SuccessfulFlap;
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

        //Initalize key press events here
        SuccessfulFlap = new UnityEvent();
    }
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            SuccessfulFlap.Invoke();
        }
	}
    #endregion
}
