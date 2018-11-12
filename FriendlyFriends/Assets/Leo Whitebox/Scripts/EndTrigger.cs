using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndTrigger : MonoBehaviour {

    static public UnityEvent GameWon;

    private void Start()
    {
        GameWon = new UnityEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            GameWon.Invoke();
        }
    }
}
