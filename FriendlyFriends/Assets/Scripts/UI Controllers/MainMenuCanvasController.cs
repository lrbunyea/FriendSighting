using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvasController : MonoBehaviour
{
    enum MenuState
    {
        title,
        levelSelect,
        fadeToLevel,
        fadetoMain
    }
    [SerializeField] GameObject l;
    [SerializeField] GameObject t;
    private float alphaT = 1;
    private float alphaL = 0;
    private CanvasGroup levelSelect;
    private CanvasGroup title;
    private Button[] levels;
    private Button start;
    private MenuState state = MenuState.title;

    #region Unity API Functions
    void Start()
    {
        //Transform l = transform.Find("LevelSelect");
        //Transform t = transform.Find("Main");
        levelSelect = l.GetComponent<CanvasGroup>();
        title = t.GetComponent<CanvasGroup>();
        levels = l.GetComponentsInChildren<Button>();
        start = t.GetComponentInChildren<Button>();

        foreach(Button b in levels)
        {
            b.interactable = false;
        }

        print(SceneManager.sceneCount);
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
                SceneManager.LoadScene("Bigfoot Caf Level");
            }
        }

        if (state == MenuState.fadeToLevel)
        {
            print("Fading to Level Select: Alpha = " + alphaT + " and " + alphaL);
            if (alphaT > 0)
            {
                print("Inside Loop");
                alphaT -= Time.deltaTime * 2.0f;
                if (alphaT < 0)
                {
                    alphaT = 0;
                }
                title.alpha = alphaT;
            }
            else
            {
                alphaL += Time.deltaTime * 2.0f;
                if (alphaL > 1)
                {
                    alphaL = 1;
                    state = MenuState.levelSelect;
                    foreach (Button b in levels)
                    {
                        b.interactable = true;
                    }
                }
                levelSelect.alpha = alphaL;
            }
        }
        if (state == MenuState.fadetoMain)
        {
            if (alphaL > 0)
            {
                alphaL -= Time.deltaTime * 2.0f;
                if (alphaL < 0)
                {
                    alphaL = 0;
                }
                levelSelect.alpha = alphaL;
            }
            else
            {
                alphaT += Time.deltaTime * 2.0f;
                if (alphaT > 1)
                {
                    alphaT = 1;
                    state = MenuState.title;
                    start.interactable = true;
                }
                title.alpha = alphaT;
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

    public void Fade()
    {
        if (state == MenuState.title)
        {
            print("Going to LevelSelect");
            state = MenuState.fadeToLevel;
            start.interactable = false;
        }
        if (state == MenuState.levelSelect)
        {
            state = MenuState.fadetoMain;
            foreach (Button b in levels)
            {
                b.interactable = false;
            }
        }
    }
    #endregion
}
