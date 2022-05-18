using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodexButtons : MonoBehaviour
{
    public string choiceText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseAnswer()
    {
        Transform parent = transform.parent;
        parent.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = choiceText; //change le texte du rapport
        parent.gameObject.SetActive(false); //désactive les boutons
    }
}
