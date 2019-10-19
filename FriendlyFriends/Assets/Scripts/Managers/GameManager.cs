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
    public bool holdingChange;
    public bool holdingChocolate;
    [SerializeField] GameObject chocolatePrefab;
    public GameObject[] objectiveList;
    private int objectiveNum;


    public UnityEvent PauseGame;
    public UnityEvent DeleteObjective;

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
            //DontDestroyOnLoad(this);
        }

        //Initialize events
        PauseGame = new UnityEvent();
        DeleteObjective = new UnityEvent();
    }

    void Start()
    {
        objectiveNum = 0;
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SetGameStateToMainMenu();
        } else
        {
            SetGameStateToGameplay();
            //UIManager.Instance.PlayTutorial1();
            UpdateObjective();
        }


        holdingChange = false;

        PauseGame.AddListener(PauseGameplay);
    }

    void Update () {
		
	}
    #endregion

    #region Helper Funtions
    public void DisableMovement()
    {
        if (im == null)
        {
            im = FindObjectOfType<InputManager>();
        }
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

    public void SetChangeHolding(bool isHolding)
    {
        holdingChange = isHolding;
    }

    public void SetChocolateHolding(bool isHolding)
    {
        holdingChocolate = isHolding;
    }

    public void SpawnChocolate()
    {
        Instantiate(chocolatePrefab);
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

    public void UpdateObjective()
    {
        UIManager.Instance.PlayTutorialNum(objectiveNum);
        if (objectiveNum < objectiveList.Length)
        {
            objectiveNum++;
        } 
    }

    public GameObject GetCurObjective()
    {
        return objectiveList[objectiveNum];
    }
    #endregion
}
