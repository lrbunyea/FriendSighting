using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NessieAnim : MonoBehaviour
{
    private Animation theAnimations;
    private Animator theAnimator;
    public AnimationClip[] theClips;
    public bool isTutorial;
    private int tutorialNum;
    // Start is called before the first frame update
    void Start()
    {
        theAnimations = this.GetComponent<Animation>();
        theAnimator = this.GetComponent<Animator>();
        tutorialNum = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialNum != 0)
        {
            tutorialNum = 0;
        }
    }


    public void progTutorial(int num)
    {
        tutorialNum = num;
        theAnimator.SetInteger(1, tutorialNum);
    }

    public void setTutorial(bool isIt)
    {
        theAnimator.SetBool(0, isIt);
    }
}
