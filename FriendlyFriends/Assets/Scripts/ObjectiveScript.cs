using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.ReferenceEquals(GameManager.Instance.GetCurObjective(), this.gameObject))
        {
            GameManager.Instance.UpdateObjective();
            Destroy(this.gameObject);
        }
    }
}
