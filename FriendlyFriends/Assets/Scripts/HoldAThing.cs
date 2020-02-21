using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HoldAThing : MonoBehaviour
{
    public GameObject theHeldObject;
    public Image itemGet;
    private bool finalRound;
    public int objectiveNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        theHeldObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Final Round")
        {
            finalRound = true;
        }
        else
        {
            finalRound = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((GameObject.ReferenceEquals(GameManager.Instance.GetCurObjective(), this.gameObject) || objectiveNum == GameManager.Instance.GetCurObjectiveNum()) && other.tag == "Player")
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            theHeldObject.SetActive(true);
            theHeldObject.GetComponent<Renderer>().enabled = true;
            GameManager.Instance.UpdateObjective();
            Destroy(this.gameObject.GetComponent<Collider>());
            StartCoroutine(getThatItem());
            if (finalRound)
            {
                ScoreManager.Instance.enableTime(true);
            }
        }
        
    }

    private IEnumerator getThatItem()
    {
        itemGet.transform.localScale = new Vector3(.01f, .01f, .01f);

        while (itemGet.GetComponent<CanvasGroup>().alpha < 1)
        {
            itemGet.GetComponent<CanvasGroup>().alpha += .1f;
            itemGet.transform.localScale += new Vector3(.1f, .1f, .1f);
            yield return new WaitForSeconds(.01f);
        }

        yield return new WaitForSeconds(1f);

        while (itemGet.GetComponent<CanvasGroup>().alpha > 0)
        {
            itemGet.GetComponent<CanvasGroup>().alpha -= .05f;
            yield return new WaitForSeconds(.001f);
        }
        Destroy(this.gameObject);
    }
}
