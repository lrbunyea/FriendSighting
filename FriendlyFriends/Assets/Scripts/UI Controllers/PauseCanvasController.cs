using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvasController : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject PausePanel;
    #endregion

    #region Unity API Functions
    void Start()
    {
        GameManager.Instance.PauseGame.AddListener(DeterminePauseScreen);
    }

     void Update()
    {
        
    }
    #endregion

    #region UI Element Functions
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.SetGameStateToMainMenu();
    }
    #endregion

    #region Event Functions
    private void DeterminePauseScreen()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Pause)
        {
            HidePauseScreen();
        } else
        {
            ShowPauseScreen();
        }
    }
    #endregion

    #region Helper Functions
    public void ShowPauseScreen()
    {
        PausePanel.SetActive(true);
        GameManager.Instance.SetGameStateToPause();
    }

    public void HidePauseScreen()
    {
        PausePanel.SetActive(false);
        GameManager.Instance.SetGameStateToGameplay();
        GameManager.Instance.ResumeGameplay();
    }
    #endregion
}
