using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour {

    // The target marker.
    public GameObject targetGO;
    private Transform target;

    //flag for input
    private bool aPressedLast;

    // Speed in units per sec.
    private float speed;

    public float speedDecrementTime;
    private float timeLeft;

    void Start()
    {
        target = targetGO.GetComponent<Transform>();
        aPressedLast = false;
        speed = 0;
        timeLeft = speedDecrementTime;
    }

    void Update()
    {
        //Decrement speed after alotted time
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && speed > 0)
        {
            speed--;
            timeLeft = speedDecrementTime;
        }

        //check key presses
        if (Input.GetKey(KeyCode.A) && aPressedLast == false)
        {
            speed++;
            aPressedLast = true;
        }
        if (Input.GetKey(KeyCode.D) && aPressedLast == true)
        {
            speed++;
            aPressedLast = false;
        }

        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
