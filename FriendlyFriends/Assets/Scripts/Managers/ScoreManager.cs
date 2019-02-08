using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text timeText;
    public Text collText;
    public int time = 0;
    public int numCollisions = 0;

    private int frames = 0;

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

    public void PlayerCollision()
    {
        numCollisions++;
        UpdateCollisions();
    }
    private void UpdateTime()
    {
        timeText.text = "Time Left: " + (120 - time);
    }
    private void UpdateCollisions()
    {
        collText.text = "Collisions: " + numCollisions;
    }
}
