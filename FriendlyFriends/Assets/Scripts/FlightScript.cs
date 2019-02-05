using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightScript : MonoBehaviour {

    #region Variables
    [SerializeField] float AmbientSpeed = 100.0f;
    [SerializeField] float flapStrength = 500.0f;
    [SerializeField] float forwardStrength = 500.0f;
    [SerializeField] float RotationSpeed = 100.0f;
    //charge values
    public float chargeStrength = 0.0f;
    public float chargeMult = 10.0f;
    //score values
    private int score = 0;
    private int obstaclesHit = 0;
    private int seconds = 0;
    private int frames = 0;
    //private int 
    #endregion

    #region Unity API Functions
    void Start()
    {
        //Register functions to events
        InputManager.Instance.SuccessfulFlap.AddListener(Flap);
        InputManager.Instance.BuildCharge.AddListener(BuildCharge);
        InputManager.Instance.ReleaseCharge.AddListener(ReleaseCharge);
    }

    void FixedUpdate()
    {
        UpdateFunction();
        UpdateScore();
    }

    void OnCollisionEnter(Collision collision)
    {
        Transform t = collision.gameObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == "Obstacle")
            {
                //print("Obstacle");
                return;
            }
            t = t.parent.transform;
        }
        //print("no tag found");
    }
    #endregion

    #region Movement Functions
    void UpdateFunction()
    {
        if (chargeStrength > 0)
            chargeStrength -= .2f;
        if (chargeStrength < 0)
            chargeStrength = 0;

        //Code modified from https://keithmaggio.wordpress.com/2011/07/01/unity-3d-code-snippet-flight-script/
        Quaternion AddRot = Quaternion.identity;
        float roll = 0;
        float pitch = 0;
        float yaw = 0;
        //roll = Input.GetAxis("Roll") * (Time.fixedDeltaTime * RotationSpeed);
        //pitch = Input.GetAxis("Pitch") * (Time.fixedDeltaTime * RotationSpeed);
        yaw = Input.GetAxis("Yaw") * (Time.fixedDeltaTime * RotationSpeed);
        AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
        GetComponent<Rigidbody>().rotation *= AddRot;
    }

    private void Flap()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, flapStrength, 0), ForceMode.Acceleration);
        GetComponent<Rigidbody>().AddForce(transform.forward * (forwardStrength * -Input.GetAxis("Pitch")), ForceMode.Force);
    }

    private void BuildCharge()
    {
        chargeStrength += 3;
    }
    private void ReleaseCharge()
    {
        print(transform.forward * (chargeStrength * chargeMult));
        GetComponent<Rigidbody>().AddForce(new Vector3(0, flapStrength * 2.0f, 0), ForceMode.Acceleration);
        GetComponent<Rigidbody>().AddForce(transform.forward * chargeStrength, ForceMode.VelocityChange);
        chargeStrength = 0;
    }
    #endregion

    #region Score Functions
    private void UpdateScore()
    {
        frames++;
        if (frames == 50)
        {
            frames = 0;
            seconds++;
        }
    }

    private void CalculateScore()
    {
        print(120 - seconds - obstaclesHit);
    }
    #endregion
}
