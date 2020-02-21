using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region Variables
    //Singleton pattern
    public static UIManager Instance;

    public GameObject[] tutorialObjects;
    public string[] objectives;

    public Image objectiveBack;
    public Text objectiveText;
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
    public void PlayTutorialNum(int tutNum)
    {
        List<GameObject> objects = new List<GameObject>();

        objectiveBack.GetComponent<CanvasGroup>().alpha = 1;
        objectiveText.GetComponent<CanvasGroup>().alpha = 1;

        for (int m = 0; m < GameObject.FindGameObjectsWithTag("Tutorial").Length; m++)
        {
            objects.Add(GameObject.FindGameObjectsWithTag("Tutorial")[0]);
        }
        for (int m = 0; m < objects.Count; m++)
        {
            Destroy(objects[m]);
        }

        Instantiate(tutorialObjects[tutNum]);
        objectiveText.text = objectives[tutNum];
    }

    #endregion
}
