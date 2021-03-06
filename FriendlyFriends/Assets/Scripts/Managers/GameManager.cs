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
    [SerializeField] string nextLevel;
    [SerializeField] int levelNum;
    public GameObject[] objectiveList;
    private int objectiveNum;
    private float endTimer = 0f;
    private SaveReader reader;

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
            for (int i = 1; i < objectiveList.Length; i++)
                objectiveList[i].SetActive(false);
            UpdateObjective();
        }


        holdingChange = false;

        PauseGame.AddListener(PauseGameplay);
        reader = new SaveReader();
        print(reader.s.ToString());

        Cursor.visible = false;
    }

    void Update () {
		if (endTimer > 0)
        {
            endTimer -= Time.deltaTime;
            if (endTimer < 1.0f)
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
	}

    void OnApplicationPause(bool pauseStatus)
    {
        print(pauseStatus);
        if (pauseStatus)
            PauseGameplay();
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
        Cursor.visible = true;
    }

    public void ResumeGameplay()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
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
            objectiveList[objectiveNum].SetActive(true);
        } 
    }

    public GameObject GetCurObjective()
    {
        return objectiveList[objectiveNum];
    }

    public int GetCurObjectiveNum()
    {
        return objectiveNum;
    }

    public void EndLevel(float score)
    {
        SaveData saved = new SaveData(reader.s.ToString());
        if (saved.GetUnlocked() == levelNum)
            saved.SetUnlocked(levelNum + 1);
        if (saved.GetScore(levelNum) > score)
        {
            saved.SetScore(levelNum, score);
        }
        reader.SaveFile(saved);
        
        endTimer = 6.0f;
    }
    #endregion
}
