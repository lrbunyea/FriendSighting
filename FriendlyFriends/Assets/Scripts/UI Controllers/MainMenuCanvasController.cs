using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvasController : MonoBehaviour
{
    #region Unity API Functions
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
        SceneManager.LoadScene("Level Layout");
        GameManager.Instance.SetGameStateToGameplay();
        UIManager.Instance.PlayTutorial1();
    }
    #endregion
}
