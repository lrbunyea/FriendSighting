using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut1TriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //UIManager.Instance.PlayTutorial2();
        Destroy(this.gameObject);
    }
}
