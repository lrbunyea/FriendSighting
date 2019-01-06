using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    //Physics values
    [SerializeField] float flapStrength;
    [SerializeField] float rightRotation;
    [SerializeField] float lerpStep = 0.07f;

    private Rigidbody playerBody;
    #endregion

    #region Unity API Functions
    void Start () {
        //Search for relevant components
        playerBody = this.GetComponent<Rigidbody>();
        //Subscribe funcitons to events
        InputManager.Instance.SuccessfulFlap.AddListener(Flap);
        InputManager.Instance.RightRotation.AddListener(RotateRight);
        InputManager.Instance.LeftRotation.AddListener(RotateLeft);
	}

    //Keeping mothboy upright should be kept in FixedUpdate
    void FixedUpdate()
    {
        Vector3 fw = transform.forward;
        fw.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, fw.normalized, lerpStep);
    }
    #endregion

    #region Movement Functions
    /// <summary>
    /// Function that is invoked when the user sucessfully inputs keys to flap
    /// Propels the character model upwards
    /// </summary>
    private void Flap()
    {
        if (InputManager.Instance.goingForwards)
        {
            playerBody.AddForce(new Vector3(0, flapStrength, 0), ForceMode.Acceleration);
            playerBody.AddForce(transform.forward * 100, ForceMode.Force);
        } else if (InputManager.Instance.goingBackwards)
        {
            playerBody.AddForce(new Vector3(0, flapStrength, 0), ForceMode.Acceleration);
            playerBody.AddForce(-transform.forward * 100, ForceMode.Force);
        } else if (InputManager.Instance.goingForwards && InputManager.Instance.goingBackwards)
        {
            playerBody.AddForce(new Vector3(0, flapStrength, 0), ForceMode.Acceleration);
        } else
        {
            playerBody.AddForce(new Vector3(0, flapStrength, 0), ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Function that is invoked when the user presses the "d" or "turn right" key
    /// Rotates the character model to the right along the Y axis
    /// </summary>
    private void RotateRight()
    {
        this.transform.Rotate(Vector3.up * Time.deltaTime * rightRotation, Space.World);
    }

    /// <summary>
    /// Function that is invoked when the user presses the "a" or "turn left" key
    /// Rotates the character model to the left along the Y axis
    /// </summary>
    private void RotateLeft()
    {
        this.transform.Rotate(Vector3.up * Time.deltaTime * -rightRotation, Space.World);
    }
    #endregion
}
