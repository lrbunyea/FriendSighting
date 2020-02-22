using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, ISelectHandler
{
    Button b;
    // Start is called before the first frame update
    void Start()
    {
        b = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        b.Select();
        print(transform.name);
    }

    public void OnSelect(BaseEventData eventData)
    {
        print(transform.name);
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        print(transform.name + " was selected");
    }
}
