using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMothmanMat : MonoBehaviour
{

    public Material whatToSwapWith;
    public Image itemGet;
    public GameObject MothyBoi;


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
        if (GameObject.ReferenceEquals(GameManager.Instance.GetCurObjective(), this.gameObject) && other.tag == "Player")
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            Material[] mats = MothyBoi.GetComponent<SkinnedMeshRenderer>().materials;
            mats[1] = whatToSwapWith;
            MothyBoi.GetComponent<SkinnedMeshRenderer>().materials = mats;
            GameManager.Instance.UpdateObjective();
            Destroy(this.gameObject.GetComponent<Collider>());
            StartCoroutine(getThatItem());
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
