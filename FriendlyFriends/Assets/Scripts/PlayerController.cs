using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    //Physics values
    [SerializeField] float flapStrength;

    private Rigidbody playerBody;
    #endregion

    #region Unity API Functions
    void Start () {
        //Search for relevant components
        playerBody = this.GetComponent<Rigidbody>();
        //Subscribe funcitons to events
        InputManager.Instance.SuccessfulFlap.AddListener(Flap);
	}
    #endregion

    #region Movement Functions
    /// <summary>
    /// Function that is invoked when the user sucessfully inputs keys to flap
    /// Propels the character model upwards
    /// </summary>
    private void Flap()
    {
        playerBody.AddForce(new Vector3(0, flapStrength, 0), ForceMode.Acceleration);
    }
    #endregion
}
