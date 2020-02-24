using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    Button b;
    // Start is called before the first frame update
    void Start()
    {
        b = GetComponent<Button>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (b.interactable)
            b.Select();
    }
}
