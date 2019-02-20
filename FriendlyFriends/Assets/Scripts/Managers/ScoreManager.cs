using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text timeText;
    public Text collText;
    public Text chargeText;
    public int seconds = 0;
    public int minutes = 0;
    public int numCollisions = 0;
    public float collSpeeds = 0;

    private int frames = 0;
    private float playerCharge = 0f;
    private bool timing = true;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateTime();
        UpdateCollisions();
        UpdateCharge();
    }

    void FixedUpdate()
    {
        if (timing) frames++;
        if (frames == 50)
        {
            frames = 0;
            seconds++;
            UpdateTime();
        }
        if (seconds == 60)
        {
            seconds = 0;
            minutes++;
            UpdateTime();
        }
    }

    public void enableTime(bool on)
    {
        timing = on;
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
        string colon = ":";
        if (seconds < 10) colon = ":0";
        timeText.text = "Time: " + minutes + colon + seconds;
    }
    private void UpdateCollisions()
    {
        float p1 = (float)(numCollisions * 5);
        //print("Collision Vals: " + p1 + " : " + (p1 + collSpeeds));
        p1 += collSpeeds;
        string value = "Student Loans: $" + p1.ToString();
        //print(value);
        value = string.Format("{0:0.00}", p1);
        collText.text = "Student Loans: $" + value;
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
}
