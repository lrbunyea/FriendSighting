using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut2TriggerController : MonoBehaviour
{
    #region Variables
    private bool hasPlayedTut3;
    private bool hasPlayedTut4;
    private bool hasPlayedTut5;
    #endregion

    #region Unity API Functions
    void Start()
    {
        hasPlayedTut3 = false;
        hasPlayedTut4 = false;
        hasPlayedTut5 = false;
    }
    #endregion


    private void OnTriggerEnter(Collider other)
    { 
        if (!hasPlayedTut3)
        {
            //UIManager.Instance.PlayTutorial3();
            hasPlayedTut3 = true;
        }

        if (hasPlayedTut3 && !hasPlayedTut4 && GameManager.Instance.holdingChange)
        {
            //UIManager.Instance.PlayTutorial4();
            hasPlayedTut4 = true;
        } else if (hasPlayedTut3 && hasPlayedTut4 && !hasPlayedTut5 && GameManager.Instance.holdingChocolate)
        {
            //UIManager.Instance.PlayTutorial5();
            Destroy(this.gameObject);
        }
        
    }
}
