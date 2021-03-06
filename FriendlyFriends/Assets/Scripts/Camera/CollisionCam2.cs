﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCam2 : MonoBehaviour
{

    public float lookUpVal = 2f;

    public float dAway;
    public float dUp = -2;
    public float smooth = 4.0f;
    public float rotateAround = 70f;
    public float subtractRotate = 217f;

    public float minDistance = 1;
    public float maxDistance = 2;


    public GameObject target;
    Transform tTrans;
    public LayerMask Occlusion;

    RaycastHit hit;
    float camHeight = 55f;
    float camPan = 0f;
    public float camRotSpeed = 217f;
    Vector3 camPos;
    Vector3 camMask;
    Vector3 followMask;

    public float hAxis;
    public float vAxis;

    void Start()
    {
        tTrans = target.transform;
        rotateAround = tTrans.eulerAngles.y + 135f;
        subtractRotate = 217f;

    }


    void LateUpdate()
    {
        hAxis = Input.GetAxis("Yaw");
        vAxis = Input.GetAxis("Vertical");

        Vector3 tOffset = new Vector3(tTrans.position.x, tTrans.position.y + 2f, tTrans.position.z);
        Quaternion rotation = Quaternion.Euler(camHeight, rotateAround, camPan);
        Vector3 vectorMask = Vector3.one;
        Vector3 rotateVector = rotation * Vector3.one;
        camPos = tOffset + (Vector3.up * dUp) - (rotateVector * dAway);
        camMask = tOffset + (Vector3.up * dUp) - (rotateVector * dAway);

        occludeRay(ref tOffset);
        smoothCamMethod();


        Vector3 newPos = tTrans.position;
        newPos.y = newPos.y + lookUpVal;
        transform.LookAt(newPos);


        rotateAround = target.GetComponent<FlightScript>().cameraSpot - subtractRotate;

        dAway = Mathf.Clamp(dAway += vAxis, minDistance, maxDistance);

    }

    void smoothCamMethod()
    {
        smooth = 4;
        transform.position = Vector3.Lerp(transform.position, camPos, Time.deltaTime * smooth);
    }

    void occludeRay(ref Vector3 targetFollow)
    {
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(targetFollow, camMask, out wallHit, Occlusion))
        {
            smooth = 4;
            camPos = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, camPos.y, wallHit.point.z + wallHit.normal.z * 0.5f);
        }
    }
}
