using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static UIManager Instance;

    [SerializeField] GameObject PauseCanvas;
    #endregion

    #region Unity API Functions
    void Awake()
    {
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
        GameManager.Instance.PauseGame.AddListener(ShowPauseScreen);
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

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void HidePauseScreen()
    {
        PauseCanvas.SetActive(false);
        GameManager.Instance.SetGameStateToGameplay();
        GameManager.Instance.ResumeGameplay();
    }
    #endregion

    #region Event Functions
    private void ShowPauseScreen()
    {
        PauseCanvas.SetActive(true);
    }
    #endregion
}
