using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingController : MonoBehaviour {

    #region Unity API Functions
    void Start () {
        FlapDown();
        //Subscribe functions to events
        InputManager.Instance.WingsUp.AddListener(FlapUp);
        InputManager.Instance.WingsDown.AddListener(FlapDown);
	}
    #endregion

    #region Flap Functions
    /// <summary>
    /// Function that is invoked when the wings flap up with player input.
    /// </summary>
    void FlapUp()
    {
        this.transform.Rotate(Vector3.right * -100, Space.World);
    }

    /// <summary>
    /// Function that is invoked when the wings flap down with player input.
    /// </summary>
    void FlapDown()
    {
        this.transform.Rotate(Vector3.right * 100, Space.World);
    }
    #endregion
}
