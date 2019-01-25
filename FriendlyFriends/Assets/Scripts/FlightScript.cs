using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightScript : MonoBehaviour {

    #region Variables
    [SerializeField] float AmbientSpeed = 100.0f;
    [SerializeField] float flapStrength = 500.0f;
    [SerializeField] float RotationSpeed = 100.0f;
    #endregion

    #region Unity API Functions
    void Start()
    {
        //Register functions to events
        InputManager.Instance.SuccessfulFlap.AddListener(Flap);
    }

    void FixedUpdate()
    {
        UpdateFunction();
    }
    #endregion

    #region Movement Functions
    void UpdateFunction()
    {
        //Code modified from https://keithmaggio.wordpress.com/2011/07/01/unity-3d-code-snippet-flight-script/
        Quaternion AddRot = Quaternion.identity;
        float roll = 0;
        float pitch = 0;
        float yaw = 0;
        roll = Input.GetAxis("Roll") * (Time.fixedDeltaTime * RotationSpeed);
        pitch = Input.GetAxis("Pitch") * (Time.fixedDeltaTime * RotationSpeed);
        yaw = Input.GetAxis("Yaw") * (Time.fixedDeltaTime * RotationSpeed);
        AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
        GetComponent<Rigidbody>().rotation *= AddRot;
    }

    private void Flap()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, flapStrength, 0), ForceMode.Acceleration);
        GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Force);
    }
    #endregion
}
