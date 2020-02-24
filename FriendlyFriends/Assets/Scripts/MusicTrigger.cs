using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    private bool switchTime;
    public AudioClip toSwitchTo;

    // Start is called before the first frame update
    void Start()
    {
        switchTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<AudioSource>().isPlaying)
        {
            if (switchTime)
            {

                this.GetComponent<AudioSource>().loop = true;
                this.GetComponent<AudioSource>().clip = toSwitchTo;
            }
            this.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switchTime = true;
        }
    }
}
