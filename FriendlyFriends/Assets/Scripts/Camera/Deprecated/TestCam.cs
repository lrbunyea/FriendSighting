using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCam : MonoBehaviour
{
    public GameObject target;

    public int rMult = 20;

    private float xAngle = 0f;
    private float yAngle = 0f;
    Vector3 offset;
    Vector3 oldTargetPos;

    // Start is called before the first frame update
    void Start()
    {
        offset = target.transform.position - transform.position;
        transform.position = target.transform.position - (offset);
        transform.LookAt(target.transform);
        oldTargetPos = target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = target.transform.position - (offset);
        //transform.LookAt(target.transform);
        if (!oldTargetPos.Equals(target.transform.position))
        {
            Vector3 difference = target.transform.position - oldTargetPos;
            transform.position += difference;
        }

        oldTargetPos = target.transform.position;
        
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if (x != 0 && (x > .1 || x < -.1))
        {
            //print(x);
            xAngle = Time.deltaTime * rMult * x;
        }
        if (y != 0 && (y > .1 || y < -.1))
        {
            //print("ymoved");
            yAngle = Time.deltaTime * rMult * y;
        }

        transform.RotateAround(target.transform.position, Vector3.up, xAngle);
        /*
        if (transform.rotation.eulerAngles.x > 0 && yAngle < 0)
            transform.RotateAround(target.transform.position, Vector3.right, yAngle);
        else if (transform.rotation.eulerAngles.x < 70 && yAngle > 0)
            transform.RotateAround(target.transform.position, Vector3.right, yAngle);
        */
        transform.RotateAround(target.transform.position, Vector3.right, yAngle);
        xAngle = 0;
        yAngle = 0;

        transform.LookAt(target.transform);
    }
}
