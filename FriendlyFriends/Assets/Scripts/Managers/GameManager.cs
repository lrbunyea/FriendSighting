using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static GameManager Instance;

    public UnityEvent PauseGame;

    public enum GameState
    {
        MainMenu,
        LevelSelect,
        Gameplay,
        Pause
    }

    public GameState currentState;
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

        //Initialize events
        PauseGame = new UnityEvent();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SetGameStateToMainMenu();
        } else
        {
            SetGameStateToGameplay();
        }
        

        PauseGame.AddListener(PauseGameplay);
    }

    void Update () {
		
	}
    #endregion

    #region Helper Funtions
    private void PauseGameplay()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGameplay()
    {
        Time.timeScale = 1f;
    }
    #endregion

    #region State Change Functions
    public void SetGameStateToMainMenu()
    {
        currentState = GameState.MainMenu;
    }

    public void SetGameStateToLevelSelect()
    {
        currentState = GameState.LevelSelect;
    }

    public void SetGameStateToGameplay()
    {
        currentState = GameState.Gameplay;
    }

    public void SetGameStateToPause()
    {
        currentState = GameState.Pause;
    }
    #endregion
}
