using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FlightScript : MonoBehaviour {

    #region Variables
    [SerializeField] float AmbientSpeed = 100.0f;
    [SerializeField] float flapStrength = 500.0f;
    [SerializeField] float forwardStrength = 500.0f;
    [SerializeField] float RotationSpeed = 100.0f;
    //charge values
    public float chargeStrength = 0.0f;
    public float chargeMult = 10.0f;

    public float cameraSpot = 0;

    AudioSource aud;
    public AudioClip[] flaps;
    public AudioClip[] crashes;
    int hitCooldown = 0;
    System.Random rand;
    #endregion

    #region Unity API Functions
    void Start()
    {
        //Register functions to events
        InputManager.Instance.SuccessfulFlap.AddListener(Flap);
        InputManager.Instance.BuildCharge.AddListener(BuildCharge);
        InputManager.Instance.ReleaseCharge.AddListener(ReleaseCharge);
        aud = GetComponent<AudioSource>();
        rand = new System.Random();
    }

    void FixedUpdate()
    {
        UpdateFunction();
        if (chargeStrength > 0)
            ScoreManager.Instance.PlayerCharge(chargeStrength);
        if (hitCooldown > 0)
            hitCooldown--;
    }

    void OnCollisionEnter(Collision collision)
    {
        Transform t = collision.gameObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == "Obstacle")
            {
                //print("Obstacle Hit");
                //collision.relativeVelocity.magnitude
                ScoreManager.Instance.PlayerCollision(collision.relativeVelocity.magnitude);
                if (hitCooldown == 0)
                {
                    hitCooldown = 25;
                    aud.PlayOneShot(crashes[rand.Next(crashes.Length)]);
                }
                return;
            }
            t = t.parent.transform;
        }
        //print(t.name);
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
        cameraSpot = GetComponent<Rigidbody>().rotation.eulerAngles.y;
    }

    private void Flap()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, flapStrength, 0), ForceMode.Acceleration);
        GetComponent<Rigidbody>().AddForce(transform.forward * (forwardStrength * -Input.GetAxis("Pitch")), ForceMode.Force);
        aud.PlayOneShot(flaps[rand.Next(flaps.Length)]);

    }

    private void BuildCharge()
    {
        chargeStrength += 3;
    }
    private void ReleaseCharge()
    {
        print(transform.forward * (chargeStrength * chargeMult));
        if (chargeStrength > 0)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, flapStrength * 1.0f, 0), ForceMode.Acceleration);
            GetComponent<Rigidbody>().AddForce(transform.forward * chargeStrength * -1f, ForceMode.VelocityChange);
        }
        chargeStrength = 0;
        ScoreManager.Instance.PlayerCharge(0);
    }
    #endregion
}
