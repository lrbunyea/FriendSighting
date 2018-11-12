using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float time = 0;
    private Text timerText;
    private bool gameWon;

    void Start()
    {
        timerText = this.GetComponent<Text>();
        gameWon = false;
        EndTrigger.GameWon.AddListener(GameHasBeenWon);
    }

    // Update is called once per frame
    void Update () {
        if (!gameWon)
        {
            time += Time.deltaTime;
            timerText.text = "Timer: " + time.ToString();
        }
	}

    private void GameHasBeenWon()
    {
        gameWon = true;
    }
}
