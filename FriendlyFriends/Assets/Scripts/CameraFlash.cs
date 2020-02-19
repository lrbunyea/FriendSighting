using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFlash : MonoBehaviour
{
    public Image theFlash;

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
        if (other.tag == "Player")
        {
            StartCoroutine(DoTheFlash());
        }
    }

    private IEnumerator DoTheFlash()
    {
        theFlash.transform.localScale = new Vector3(.01f, .01f, .01f);

        GetComponent<AudioSource>().Play();

        while (theFlash.GetComponent<CanvasGroup>().alpha < 1)
        {
            theFlash.GetComponent<CanvasGroup>().alpha += .2f;
            theFlash.transform.localScale += new Vector3(.3f, .3f, .3f);
            yield return new WaitForSeconds(.005f);
        }

        yield return new WaitForSeconds(.2f);

        while (theFlash.GetComponent<CanvasGroup>().alpha > 0)
        {
            theFlash.GetComponent<CanvasGroup>().alpha -= .05f;
            yield return new WaitForSeconds(.08f);
        }
        Destroy(this.gameObject);
    }


}
