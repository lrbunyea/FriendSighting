using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static UIManager Instance;
    #endregion

    #region Unity API Functions
    void Awake()
    {
        //Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region UI Element Functions
    public void StartButtonPressed()
    {
        SceneManager.LoadScene("ControlTest1");
    }
    #endregion
}
