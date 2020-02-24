using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSlider : MonoBehaviour, IPointerEnterHandler
{
    Slider s;

    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<Slider>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (s.interactable)
            s.Select();
    }

}
