using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut2TriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.PlayTutorial3();
        Destroy(this.gameObject);
    }
}
