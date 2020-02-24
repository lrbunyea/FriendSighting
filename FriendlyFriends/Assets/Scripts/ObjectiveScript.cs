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
            if (this.gameObject.tag == "end" && other.tag == "Player")
            {
                ScoreManager.Instance.EndScore();
                Destroy(this.gameObject.GetComponent<Collider>());
                if (transform.Find("ObjectiveParticles") != null)
                {
                    transform.Find("ObjectiveParticles").gameObject.SetActive(false);
                }
            }
            else if (other.tag == "Player")
            {
                GameManager.Instance.UpdateObjective();
                Destroy(this.gameObject.GetComponent<Collider>());
                if (transform.Find("ObjectiveParticles") != null)
                {
                    transform.Find("ObjectiveParticles").gameObject.SetActive(false);
                }
            }

            
        }
    }
}
