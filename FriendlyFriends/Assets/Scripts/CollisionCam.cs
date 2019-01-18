using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCam : MonoBehaviour {

    public float dAway;
    public float dUp = -2;
    public float smooth = 4.0f;
    public float rotateAround = 70f;

    public float minDistance = 1;
    public float maxDistance = 2;


    public GameObject target;
    Transform tTrans;
    public LayerMask Occlusion;

    //public worldVectorMap wvm;

    RaycastHit hit;
    float camHeight = 55f;
    float camPan = 0f;
    float camRotSpeed = 180f;
    Vector3 camPos;
    Vector3 camMask;
    Vector3 followMask;

    public float hAxis;
    public float vAxis;
	// Use this for initialization
	void Start () {
        tTrans = target.transform;
        rotateAround = tTrans.eulerAngles.y - 45f;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate(){
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        Vector3 tOffset = new Vector3(tTrans.position.x, tTrans.position.y + 2f, tTrans.position.z);
        Quaternion rotation = Quaternion.Euler(camHeight, rotateAround, camPan);
        Vector3 vectorMask = Vector3.one;
        Vector3 rotateVector = rotation * Vector3.one;
        camPos = tOffset + (Vector3.up * dUp) - (rotateVector * dAway);
        camMask = tOffset + (Vector3.up * dUp) - (rotateVector * dAway);

        occludeRay(ref tOffset);
        smoothCamMethod();

        transform.LookAt(tTrans);
        if (rotateAround > 360)
            rotateAround = 0f;
        else if (rotateAround < 0)
            rotateAround += 360f;

        rotateAround += hAxis * camRotSpeed * Time.deltaTime;
        dAway = Mathf.Clamp(dAway += vAxis, minDistance, maxDistance);

    }

    void smoothCamMethod()
    {
        smooth = 4f;
        transform.position = Vector3.Lerp(transform.position, camPos, Time.deltaTime * smooth);
    }

    void occludeRay(ref Vector3 targetFollow)
    {
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(targetFollow, camMask, out wallHit, Occlusion))
        {
            smooth = 10f;
            camPos = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, camPos.y, wallHit.point.z + wallHit.normal.z * 0.5f);
        }
    }
}
