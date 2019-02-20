using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    #region Variables
    //Singleton pattern
    public static UIManager Instance;

    //Tutroial Dialogue prefabs
    [SerializeField] GameObject tut1;
    [SerializeField] GameObject tut2;
    [SerializeField] GameObject tut3;
    [SerializeField] GameObject tut4;
    [SerializeField] GameObject tut5;
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
    }

    void Update()
    {

    }
    #endregion

    #region Tutorial Dialogue Functions
    public void PlayTutorial1()
    {
        Instantiate(tut1);
        GameManager.Instance.DisableMovement();
        ScoreManager.Instance.enableTime(false);
    }

    public void PlayTutorial2()
    {
        Instantiate(tut2);
        GameManager.Instance.DisableMovement();
        ScoreManager.Instance.enableTime(false);
    }

    public void PlayTutorial3()
    {
        Instantiate(tut3);
        GameManager.Instance.DisableMovement();
        ScoreManager.Instance.enableTime(false);
    }

    public void PlayTutorial4()
    {
        Instantiate(tut4);
        GameManager.Instance.DisableMovement();
        ScoreManager.Instance.enableTime(false);
    }

    public void PlayTutorial5()
    {
        Instantiate(tut5);
        //GameManager.Instance.DisableMovement();
        ScoreManager.Instance.enableTime(false);
    }
    #endregion
}
