using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFan : MonoBehaviour
{
    Transform t;
    public float rotateMax = 1.0f;
    public float rotateRate = 0f;
    public float timeToMax = 3.0f;
    private bool spinning = false;
    // Start is called before the first frame update
    void Start()
    {
        t = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            spinning = !spinning;
        }
        */
        if (spinning)
        {
            if (rotateRate < rotateMax)
                rotateRate += Time.deltaTime * rotateMax / timeToMax;
            if (rotateRate > rotateMax)
                rotateRate = rotateMax;
        }
        else
        {
            if (rotateRate > 0)
                rotateRate -= Time.deltaTime * rotateMax / timeToMax;
            if (rotateRate < 0)
                rotateRate = 0;
        }

        Vector3 currentAngle = t.rotation.eulerAngles;
        currentAngle.y += rotateRate * 360f * Time.deltaTime;
        t.eulerAngles = currentAngle;
    }

    public bool IsSpinning()
    {
        return spinning;
    }

    public void SetSpinning(bool spin)
    {
        spinning = spin;
    }
}
