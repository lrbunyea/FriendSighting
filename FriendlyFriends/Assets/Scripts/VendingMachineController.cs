using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.holdingChange)
        {
            GameManager.Instance.DeleteObjective.Invoke();
            GameManager.Instance.SpawnChocolate();
            Destroy(this);
        }
    }
}
