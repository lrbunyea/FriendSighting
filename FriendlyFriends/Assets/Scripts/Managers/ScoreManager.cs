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
    public int time = 0;
    public int numCollisions = 0;

    private int frames = 0;
    private float playerCharge = 0f;

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
        frames++;
        if (frames == 50)
        {
            frames = 0;
            time++;
            UpdateTime();
        }
    }

    public void PlayerCollision(float magnitude)
    {
        numCollisions++;
        UpdateCollisions();
        print("Speed of Collision: " + magnitude);
    }
    public void PlayerCharge(float chargeStrength)
    {
        playerCharge = chargeStrength;
        UpdateCharge();
    }

    private void UpdateTime()
    {
        timeText.text = "Time Left: " + (120 - time);
    }
    private void UpdateCollisions()
    {
        collText.text = "Collisions: " + numCollisions;
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
