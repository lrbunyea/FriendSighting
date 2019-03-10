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
            //DontDestroyOnLoad(this);
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
        //GameManager.Instance.DisableMovement();
        //ScoreManager.Instance.enableTime(false);
    }

    public void PlayTutorial2()
    {
        List<GameObject> objects = new List<GameObject>();

        for (int m = 0; m < GameObject.FindGameObjectsWithTag("Tutorial").Length; m++)
        {
            objects.Add(GameObject.FindGameObjectsWithTag("Tutorial")[0]);
        }
        for (int m = 0; m < objects.Count; m++)
        {
            Destroy(objects[m]);
        }
        Instantiate(tut2);
        //GameManager.Instance.DisableMovement();
        ScoreManager.Instance.enableTime(true);

    }

    public void PlayTutorial3()
    {
        List<GameObject> objects = new List<GameObject>();

        for (int m = 0; m < GameObject.FindGameObjectsWithTag("Tutorial").Length; m++)
        {
            objects.Add(GameObject.FindGameObjectsWithTag("Tutorial")[m]);
        }
        for (int m = 0; m < objects.Count; m++)
        {
            Destroy(objects[m]);
        }

        Instantiate(tut3);
        //GameManager.Instance.DisableMovement();
        //ScoreManager.Instance.enableTime(false);
        
    }

    public void PlayTutorial4()
    {
        List<GameObject> objects = new List<GameObject>();

        for (int m = 0; m < GameObject.FindGameObjectsWithTag("Tutorial").Length; m++)
        {
            objects.Add(GameObject.FindGameObjectsWithTag("Tutorial")[0]);
        }
        
        for (int m = 0; m < objects.Count; m++)
        {
            Destroy(objects[m]);
        }
        Instantiate(tut4);
        //GameManager.Instance.DisableMovement();
        //ScoreManager.Instance.enableTime(false);
    }

    public void PlayTutorial5()
    {

        List<GameObject> objects = new List<GameObject>();

        for (int m = 0; m < GameObject.FindGameObjectsWithTag("Tutorial").Length; m++)
        {
            objects.Add(GameObject.FindGameObjectsWithTag("Tutorial")[0]);
        }
        for (int m = 0; m < objects.Count; m++)
        {
            Destroy(objects[m]);
        }
        Instantiate(tut5);
        //GameManager.Instance.DisableMovement();
        //ScoreManager.Instance.enableTime(false);
        ScoreManager.Instance.EndScore();
    }
    #endregion
}
