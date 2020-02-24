using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class PauseCanvasController : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject PausePanel;
    [SerializeField] EventSystem ev;
    [SerializeField] AudioMixer mixer;
    CanvasGroup pause;
    CanvasGroup options;
    Button[] pauseButtons;
    Button optionBack;
    Slider sound;
    Slider music;
    #endregion

    #region Unity API Functions
    void Start()
    {
        GameManager.Instance.PauseGame.AddListener(DeterminePauseScreen);

        PausePanel.SetActive(true);
        pauseButtons = PausePanel.transform.GetChild(0).GetComponentsInChildren<Button>();
        pause = PausePanel.transform.GetChild(0).GetComponent<CanvasGroup>();
        options = PausePanel.transform.GetChild(1).GetComponent<CanvasGroup>();
        Transform optionMenu = PausePanel.transform.GetChild(1);
        sound = optionMenu.GetChild(0).GetComponent<Slider>();
        music = optionMenu.GetChild(1).GetComponent<Slider>();
        optionBack = optionMenu.GetChild(2).GetComponent<Button>();

        float soundVal = 0;
        mixer.GetFloat("SoundVolume", out soundVal);
        float musicVal = 0;
        mixer.GetFloat("MusicVolume", out musicVal);

        sound.value = ConvertDbToFloat(soundVal);
        music.value = ConvertDbToFloat(musicVal);

        PausePanel.SetActive(false);
    }

    void Update()
    {
        if (PausePanel.activeInHierarchy)
        {
            if (ev.currentSelectedGameObject == null)
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    SelectDefaultButton();
                }
            }
        }
    }
    #endregion

    #region UI Element Functions
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
        //GameManager.Instance.SetGameStateToMainMenu();
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
    }

    public void ToOptions()
    {
        pause.alpha = 0;
        options.alpha = 1;
        foreach(Button b in pauseButtons)
        {
            b.interactable = false;
        }
        optionBack.interactable = true;
        sound.interactable = true;
        music.interactable = true;
    }

    public void ToPause()
    {
        pause.alpha = 1;
        options.alpha = 0;
        foreach (Button b in pauseButtons)
        {
            b.interactable = true;
        }
        optionBack.interactable = false;
        sound.interactable = false;
        music.interactable = false;
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
        ToPause();
        GameManager.Instance.SetGameStateToPause();
    }

    public void HidePauseScreen()
    {
        ToPause();

        PausePanel.SetActive(false);
        GameManager.Instance.SetGameStateToGameplay();
        GameManager.Instance.ResumeGameplay();
    }

    void SelectDefaultButton()
    {
        if (pause.alpha > 0)
        {
            pauseButtons[0].Select();
        }
        else
        {
            sound.Select();
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
