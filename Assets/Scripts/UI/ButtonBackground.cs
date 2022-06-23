using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBackground : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        bg = transform.GetChild(0).gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.SetActive(false);
    }
}
