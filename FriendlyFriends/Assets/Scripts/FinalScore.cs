using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    SaveReader reader;
    public Text loanText;
    // Start is called before the first frame update
    void Start()
    {
        float sum = 0;
        reader = new SaveReader();
        for (int i = 0; i < 5; i++)
        {
            sum += reader.s.GetScore(i);
        }
        loanText.text = "$" + string.Format("{0:0.00}", sum);
    }


}
