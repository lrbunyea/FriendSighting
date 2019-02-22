using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class sideDialogue : MonoBehaviour {

    private List<string> sentences;
    private string numOfText;
    private List<GameObject> theTextBoxes; //this is going to be a list of theTextPrefabs.
    public GameObject theTextPrefab; //this is the prefab for the text box things with the little avatar next to them. Currently that prefab is
    //called TextPrefab and is in the Prefabs folder. This prefab needs to have an image, semitransparent back, and text box associated with it.
    public GameObject theTextBox; //this is the box that the individual text messages appear in. It dictates where theTextPrefab appears
    //and how it fades out.
    public GameObject theCanvas; //the text box and text prefabs should be on their own canvas. Yay.
    public TextAsset theConvoFam; //this is the text document that the conversation is drawn from. Each new line is a different text box.
    AudioSource aud;


	// Use this for initialization
	void Start () {

        sentences = new List<string>();
        theTextBoxes = new List<GameObject>();

        aud = GetComponent<AudioSource>();
        aud.volume = .2f;
        StringReader tr = null;
        string sTemp;
        tr = new StringReader(theConvoFam.text);
        while ((sTemp = tr.ReadLine()) != null)
        {
            //Debug.Log(sTemp);
            sentences.Add(sTemp);
        }

        //basically all the stringreader stuff takes each individual line in the theConvoFam and adds it to the list of stuff to be read

        
        StartCoroutine(theConvo());


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator theConvo()
    {
        GameObject currentTextBox; //we'll be using this a lot as a placeholder text box

        float textDisplayLen;

        float width = theTextBox.GetComponent<RectTransform>().sizeDelta.x; //the width of the area that the text prefabs appear in
        float height = theTextBox.GetComponent<RectTransform>().rect.height; //the height of the area that the text prefabs appear in
        Vector3 pos = theTextBox.GetComponent<RectTransform>().anchoredPosition; //the position of the area that the text prefabs appear in

        float height2 = theTextPrefab.transform.GetChild(0).GetComponent<Image>().GetComponent<RectTransform>().rect.height + 7;

        for (int i = 0; i < sentences.Count; i++)
        {
            string [] splitBoyz;
            //okay, so here's the deal. basically, by formatting your text document right, this script will get who you're talking to and
            //show the correct avatar associated with the person talking so you don't have to worry about it. You just have to format
            //your text document like this. If nessie's talking, you do
            //NessieImg::hey y'all what's up
            //with NessieImg being whatever your image for nessie's avatar is called. You will need to make a folder called Resources and
            //put all the avatar images in there, but there may be a way around that. I haven't found it yet.
            //The double colon is very important, because that's where it's gonna split the text. It needs to be a double colon, otherwise
            //it won't do it.

            splitBoyz = sentences[i].Split(new string[] { "::" }, System.StringSplitOptions.None);
            //splits the string and puts it in splitBoyz. splitBoyz[0] contains the name of the avatar, splitBoyz[1] contains the actual
            //sentence.

            currentTextBox = Instantiate(theTextPrefab, theCanvas.transform);
            currentTextBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(pos.x + 100f, 50f);
            //It makes the text box prefab in the correct position.

            currentTextBox.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/"+splitBoyz[0]);
            //this is the line that loads the avatar and puts it in image for the text box prefab.

            theTextBoxes.Add(currentTextBox);

            //adds the currentTextBox to the list of text boxes to be shown

            foreach (char letter in splitBoyz[1].ToCharArray())
            {
                currentTextBox.transform.GetChild(1).GetComponent<Text>().text += letter;
                aud.Play();
                yield return new WaitForSeconds(.02f); // this is how long it takes each letter to type, change the parameter to something else
            }



            if ((.03f * sentences[i].Length) > 0.7f)
            {
                textDisplayLen = .03f * sentences[i].Length;
            }

            else
            {
                textDisplayLen = .7f;
            }

            //the if-else statement decides how long to wait on the sentence before typing out the next one. This one's calculated based on
            //how many letters with the shortest amount of time a sentence can be shown as .7f, but you can mix it up if you want.

          

            yield return new WaitForSeconds(textDisplayLen);

            //this is just to actually, y'know, wait for the amount of seconts we calculated.

            if (i >= 0)
            {
                for (int d = 0; d < theTextBoxes.Count; d++)
                {


                    if (theTextBoxes.Count >= 4)
                    {
                        
                        while (theTextBoxes[0].GetComponent<CanvasGroup>().alpha > 0)
                        {
                            theTextBoxes[0].GetComponent<CanvasGroup>().alpha -= .1f;
                            yield return new WaitForSeconds(0.005f);
                        }
                        Destroy(theTextBoxes[0]);
                        theTextBoxes.RemoveAt(0);
                        //Fades out the oldest text prefab, removes it from the list, and destroys it.
                    }

                    Vector3 ogPos = theTextBoxes[d].GetComponent<RectTransform>().anchoredPosition;
                    float ogAlpha = theTextBoxes[d].GetComponent<CanvasGroup>().alpha;


                    while (theTextBoxes[d].GetComponent<RectTransform>().anchoredPosition.y < ogPos.y + (height2))
                    {
                        theTextBoxes[d].GetComponent<RectTransform>().anchoredPosition = new Vector3(theTextBoxes[d].GetComponent<RectTransform>().anchoredPosition.x,
                        theTextBoxes[d].GetComponent<RectTransform>().anchoredPosition.y + 3f); //shifts every text box up by the amount necessary
                        if (d + 1 < theTextBoxes.Count)
                        {
                            theTextBoxes[d + 1].GetComponent<RectTransform>().anchoredPosition = new Vector3(theTextBoxes[d].GetComponent<RectTransform>().anchoredPosition.x,
                            theTextBoxes[d + 1].GetComponent<RectTransform>().anchoredPosition.y + 3f); //shifts every text box up by the amount necessary
                            if (d + 2 < theTextBoxes.Count)
                            {
                                theTextBoxes[d + 2].GetComponent<RectTransform>().anchoredPosition = new Vector3(theTextBoxes[d].GetComponent<RectTransform>().anchoredPosition.x,
                                theTextBoxes[d + 2].GetComponent<RectTransform>().anchoredPosition.y + 3f); //shifts every text box up by the amount necessary
                            }
                        }

                        if (theTextBoxes[d].GetComponent<CanvasGroup>().alpha > (ogAlpha - .25f))
                        {
                            theTextBoxes[d].GetComponent<CanvasGroup>().alpha -= .1f;
                            if (d + 1 < theTextBoxes.Count)
                            {
                                theTextBoxes[d + 1].GetComponent<CanvasGroup>().alpha -= .1f;
                                if (d + 2 < theTextBoxes.Count)
                                {
                                    theTextBoxes[d + 2].GetComponent<CanvasGroup>().alpha -= .1f;
                                }
                            }
                            //lowers the alpha of every text box by .25f
                        }
                        yield return new WaitForSeconds(0.005f);
                    }
                    
                    d += 2;
                    //okay I'm not gonna lie I don't know why I have to add 2 to d but I know that that does have to happen or everything
                    //goes wACK so be careful if you're gonna mess with this lad

                }

            }
            
        }
        //GameManager.Instance.EnableMovement();
        //ScoreManager.Instance.enableTime(true);
        Destroy(this.gameObject);
    }
}
