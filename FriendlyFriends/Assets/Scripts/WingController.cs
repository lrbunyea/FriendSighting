using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingController : MonoBehaviour {

    #region Variables
    [SerializeField] Animator anim;
    #endregion

    #region Unity API Functions
    void Start () {
        FlapDown();
        //Subscribe functions to events
        InputManager.Instance.WingsUp.AddListener(FlapUp);
        InputManager.Instance.WingsDown.AddListener(FlapDown);

        anim = GetComponent<Animator>();
	}
    #endregion

    #region Flap Functions
    /// <summary>
    /// Function that is invoked when the wings flap up with player input.
    /// </summary>
    void FlapUp()
    {
        anim.SetBool("FlapUp", true);
    }

    /// <summary>
    /// Function that is invoked when the wings flap down with player input.
    /// </summary>
    void FlapDown()
    {
        anim.SetBool("FlapUp", false);
    }
    #endregion
}
