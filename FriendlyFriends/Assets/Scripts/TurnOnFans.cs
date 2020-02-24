using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnFans : MonoBehaviour
{
    public CeilingFan[] fans;
    public ParticleSystem part;
    public AudioClip fanStart;
    public AudioClip fanLoop;
    AudioSource[] auds;
    bool fanStarted = false;
    void Start()
    {
        auds = GetComponents<AudioSource>();
        auds[0].loop = false;
        auds[1].loop = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!fanStarted)
            {
                auds[0].PlayOneShot(fanStart);
                auds[1].clip = fanLoop;
                auds[1].PlayDelayed(4.0f);
                fanStarted = true;
            }
            
            //turn on all the fans
            foreach (CeilingFan f in fans)
            {
                f.SetSpinning(true);
            }
            //Get rid of the steam wall blocking further progress
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
                part.Stop();
            }
        }

    }
}
