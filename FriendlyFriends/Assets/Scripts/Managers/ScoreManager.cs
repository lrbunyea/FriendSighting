using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text timeText;
    public Text collText;
    public Text chargeText;
    public Text finalTime;
    public Text finalScore;
    public CanvasGroup regScore;
    public CanvasGroup endLevel;
    public int seconds = 0;
    public int minutes = 0;
    public int numCollisions = 0;
    public float collSpeeds = 0;

    private int frames = 0;
    private float playerCharge = 0f;
    private bool timing = true;
    private string propertyDamageorLoans;
    private float amountOfSeconds;
    private float amountOfMinutes;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        if (SceneManager.GetActiveScene().name == "Final Round")
        {
            propertyDamageorLoans = "Property Damage: $";
        }
        else
        {
            propertyDamageorLoans = "Student Loans: $";
        }
        amountOfMinutes = 1;
        amountOfSeconds = 30;
        enableTime(false);
        timeText.GetComponent<CanvasGroup>().alpha = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        //UpdateTime();
        UpdateCollisions();
        //UpdateCharge();
        TurnOnOffCanvasGroup(regScore, true);
        TurnOnOffCanvasGroup(endLevel, false);
        
    }

    void FixedUpdate()
    {
        if (timing) frames++;
        if (frames == 50)
        {
            frames = 0;
            amountOfSeconds--;
            seconds++;
            UpdateTime();
        }
       /* if (seconds == 60)
        {
            seconds = 0;
            minutes++;
            UpdateTime();
        }
        */
        if (amountOfSeconds == 0)
        {
            if (amountOfMinutes != 0)
            {
                amountOfMinutes -= 1;
                amountOfSeconds = 60;
                UpdateTime();
            }
            else
            {
                EndScore();
            }
        }
    }

    public void enableTime(bool on)
    {
        timing = on;
        timeText.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void PlayerCollision(float magnitude)
    {
        numCollisions++;
        collSpeeds += magnitude;
        UpdateCollisions();
        //print("Speed of Collision: " + magnitude);
    }
    public void PlayerCharge(float chargeStrength)
    {
        playerCharge = chargeStrength;
        UpdateCharge();
    }

    private void UpdateTime()
    {
        if (propertyDamageorLoans == "Property Damage: $")
        {
            string colon = ":";
            if (amountOfSeconds < 10) colon = ":0";
            timeText.text = "Time: " + amountOfMinutes + colon + amountOfSeconds;
        }

    }
    private void UpdateCollisions()
    {
        float p1 = (float)(numCollisions * 5);
        //print("Collision Vals: " + p1 + " : " + (p1 + collSpeeds));
        p1 += collSpeeds;
        string value = propertyDamageorLoans + p1.ToString();
        //print(value);
        if (SceneManager.GetActiveScene().name == "Final Round" && !timing)
        {
            p1 = 0;
        }
        value = string.Format("{0:0.00}", p1);
        collText.text = propertyDamageorLoans + value;
    }
    private void UpdateCharge()
    {
        int numbars = (int)((20.0 / 50.0) * playerCharge);
        string bar = "";
        for (int i = 0; i < numbars; i++)
        {
            bar = bar + "I";
        }
        chargeText.text = bar;
    }

    public void EndScore()
    {

        TurnOnOffCanvasGroup(regScore, false);
        TurnOnOffCanvasGroup(endLevel, true);
        //finalTime.text = timeText.text;
        finalScore.text = collText.text;

    }

    private void TurnOnOffCanvasGroup(CanvasGroup theGroup, bool isOn)
    {
        if (isOn)
        {
            theGroup.alpha = 1f;
        }
        else
        {
            theGroup.alpha = 0f;
        }
    }
   
}
