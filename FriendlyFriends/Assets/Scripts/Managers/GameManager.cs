using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static GameManager Instance;
    #endregion

    #region Unity API Functions
    void Awake () {
        //Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        
    }

    void Update () {
		
	}
    #endregion
}
