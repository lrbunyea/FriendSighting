using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static GameManager Instance;

    private InputManager im;

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
            UIManager.Instance.PlayTutorial1();
        }
        

        PauseGame.AddListener(PauseGameplay);
    }

    void Update () {
		
	}
    #endregion

    #region Helper Funtions
    public void DisableMovement()
    {
        im = FindObjectOfType<InputManager>();
        im.gameObject.SetActive(false);
    }

    public void EnableMovement()
    {
        im.gameObject.SetActive(true);
    }

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
