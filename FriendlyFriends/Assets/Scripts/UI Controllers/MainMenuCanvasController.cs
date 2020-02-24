using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuCanvasController : MonoBehaviour
{
    enum MenuState
    {
        title = 0,
        levelSelect = 1,
        options = 2,
        fade,
        fadeToLevel,
        fadetoMain
    }
    [SerializeField] GameObject l;
    [SerializeField] GameObject t;
    [SerializeField] GameObject o;
    [SerializeField] UnityEngine.EventSystems.EventSystem ev;
    [SerializeField] AudioMixer mixer;
    private CanvasGroup levelSelect;
    private CanvasGroup titleScreen;
    private CanvasGroup optionMenu;
    private CanvasGroup[] screens;
    private Button[] levelButtons;
    private Button[] mainButtons;
    private Button[] optionButtons;
    private Slider[] optionSliders;
    private int fadingTo = -1;
    private int fadingFrom = -1;
    private float alphaTo = 0;
    private float alphaFrom = 1;
    private SaveReader reader;
    
    private MenuState state = MenuState.title;

    #region Unity API Functions
    void Start()
    {
        print(Application.persistentDataPath + "/save.dat");

        levelSelect = l.GetComponent<CanvasGroup>();
        titleScreen = t.GetComponent<CanvasGroup>();
        optionMenu = o.GetComponent<CanvasGroup>();
        levelButtons = l.GetComponentsInChildren<Button>();
        mainButtons = t.GetComponentsInChildren<Button>();
        optionButtons = o.GetComponentsInChildren<Button>();
        optionSliders = o.GetComponentsInChildren<Slider>();
        screens = new CanvasGroup[3];
        screens[0] = titleScreen;
        screens[1] = levelSelect;
        screens[2] = optionMenu;


        float soundVal = 0;
        mixer.GetFloat("SoundVolume", out soundVal);
        float musicVal = 0;
        mixer.GetFloat("MusicVolume", out musicVal);

        optionSliders[0].value = ConvertDbToFloat(soundVal);
        optionSliders[1].value = ConvertDbToFloat(musicVal);

        reader = new SaveReader();

        foreach (Button b in levelButtons)
        {
            b.interactable = false;
        }
        foreach(Button b in optionButtons)
        {
            b.interactable = false;
        }
    }
    
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (ev.currentSelectedGameObject == null)
            {
                SelectDefaultButton();
            }
        }
        /*
        if (Input.GetKeyDown("joystick button 0"))
            {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                if (state == MenuState.title)
                    FadeTo(1);
            }
            else if (SceneManager.GetActiveScene().name == "Letter")
            {
                SceneManager.LoadScene("Bigfoot Caf Level");
            }
        }
        */
        if (state == MenuState.fade)
        {
            if (alphaFrom > 0)
            {
                alphaFrom -= Time.deltaTime * 2.0f;
                if (alphaFrom < 0)
                {
                    alphaFrom = 0;
                }
                screens[fadingFrom].alpha = alphaFrom;
            }
            else
            {
                alphaTo += Time.deltaTime * 2.0f;
                if (alphaTo > 1)
                {
                    alphaTo = 1.0f;
                    state = (MenuState)fadingTo;
                    InteractButtons(true);
                }
                screens[fadingTo].alpha = alphaTo;
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
    
    public void FadeTo(int i)
    {
        if (state != MenuState.fade)
        {
            alphaTo = 0;
            alphaFrom = 1.0f;
            fadingTo = i;
            fadingFrom = (int)state;
            InteractButtons(false);

            state = MenuState.fade;

        }
    }


    public void AdjustSound(float val)
    {
        mixer.SetFloat("SoundVolume", ConvertToDecibel(val));
    }

    public void AdjustMusic(float val)
    {
        mixer.SetFloat("MusicVolume", ConvertToDecibel(val));
    }
    #endregion

    #region Helper Functions
    private void InteractButtons(bool interactive)
    {
        Button[] tempButtons = new Button[0];
        if (state == MenuState.title)
        {
            tempButtons = mainButtons;
        }
        else if (state == MenuState.levelSelect)
        {
            tempButtons = levelButtons;
        }
        else if (state == MenuState.options)
        {
            tempButtons = optionButtons;
            foreach(Slider s in optionSliders)
            {
                s.interactable = interactive;
            }
        }
        foreach (Button b in tempButtons)
        {
            b.interactable = interactive;
        }

        if (state == MenuState.levelSelect)
            SetUnlockedLevels();
    }

    private void SelectDefaultButton()
    {
        if (state == MenuState.title)
        {
            mainButtons[0].Select();
        }
        else if (state == MenuState.levelSelect)
        {
            levelButtons[0].Select();
        }
        else if (state == MenuState.options)
        {
            optionSliders[0].Select();
        }
    }

    private void SetUnlockedLevels()
    {
        int unlocked = reader.s.GetUnlocked();
        for (int i = 0; i < 5; i++)
        {
            if (i <= unlocked)
                levelButtons[i].interactable = true;
            else
                levelButtons[i].interactable = false;
        }
    }

    private float ConvertToDecibel(float value)
    {
        return Mathf.Log10(Mathf.Max(value, .0001f)) * 20f;
    }
    private float ConvertDbToFloat(float value)
    {
        return Mathf.Pow(10, (value / 20.0f));
    }
    #endregion
}
