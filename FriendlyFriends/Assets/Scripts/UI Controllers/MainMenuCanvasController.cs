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
        if (Input.GetKeyDown("joystick button 0"))
            {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                SceneManager.LoadScene("Letter");
            }
            else if (SceneManager.GetActiveScene().name == "Letter")
            {
                SceneManager.LoadScene("Level Layout (PC)");
            }
        }
    }
    #endregion

    #region UI Element Functions
    public void StartButtonPressed(string whatScene)
    {
        SceneManager.LoadScene(whatScene);
        //GameManager.Instance.SetGameStateToGameplay();
        //UIManager.Instance.PlayTutorial1();
    }

    #endregion
}
