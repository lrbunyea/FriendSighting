using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChocolateController : MonoBehaviour
{
    #region Variables
    private bool pressedKey;
    //private bool beingHeld;
    private Rigidbody rb;
    #endregion
    AudioSource aud;
    public AudioClip pickup;
    public AudioClip putdown;

    #region Unity API Functions

    void Awake()
    {
        HoldAThing hold = GetComponent<HoldAThing>();
        hold.itemGet = GameObject.Find("Candy Get").GetComponent<Image>();
        hold.theHeldObject = GameObject.FindWithTag("Candy").gameObject;
        hold.objectiveNum = 4;
    }
    private void Start()
    {
        pressedKey = false;
        //beingHeld = false;
        rb = GetComponentInParent<Rigidbody>();
        GameManager.Instance.DeleteObjective.AddListener(DeleteOnEvent);

        rb.AddForce(new Vector3(1, 0, 0)*1000, ForceMode.Acceleration);
        //aud = GetComponent<AudioSource>();


    }

    private void Update()
    {
        /*
        //Check if Player is currently holding the object
        if (beingHeld)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Clearing parent");
                transform.parent = null;
                beingHeld = false;
                rb.isKinematic = false;
                rb.detectCollisions = true;
                //rb.useGravity = true;
                GameManager.Instance.SetChocolateHolding(false);
                aud.PlayOneShot(putdown);
            }
        }
        */
        //Logic for item gathering key presses
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
        {
            pressedKey = true;
        }
        else if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("Fire1"))
        {
            pressedKey = false;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        /*
        //Debug.Log("Trigger entered!");
        if (pressedKey && other.gameObject.tag == "Player")
        {
            Debug.Log("Object picked up!");
            transform.SetParent(other.gameObject.transform);
            //beingHeld = true;
            rb.isKinematic = true;
            rb.detectCollisions = false;
            //rb.useGravity = false;
            GameManager.Instance.SetChocolateHolding(true);
            pressedKey = false;
            aud.PlayOneShot(pickup);
        }
        */
    }
    #endregion

    public void DeleteOnEvent()
    {
        GameManager.Instance.SetChocolateHolding(false);
        Destroy(this.gameObject);
    }
}
