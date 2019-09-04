using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
    public GameObject target;
    public float damp = 1f;
    Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = target.transform.position - transform.position;
        //damp = 1f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float mAngle = transform.eulerAngles.y;
        float tAngle = target.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(mAngle, tAngle, Time.deltaTime * damp);

        Quaternion rotate = Quaternion.Euler(0, angle, 0);

        //Vector3 now = Vector3.Lerp(transform.position, target.transform.position - offset, Time.deltaTime * damp);
        //Vector3 pos = transform.position;
        //pos.y = target.transform.position.y - offset.y;
        transform.position = target.transform.position - (rotate * offset);
        

        transform.LookAt(target.transform);
    }
}
