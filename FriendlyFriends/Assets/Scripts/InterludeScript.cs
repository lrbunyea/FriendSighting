using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterludeScript : MonoBehaviour
{
    [SerializeField] string nextLevel;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0") || Input.GetButtonDown("FlapUp") || Input.GetButtonDown("FlapDown") || Input.GetButtonDown("Pause"))
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
