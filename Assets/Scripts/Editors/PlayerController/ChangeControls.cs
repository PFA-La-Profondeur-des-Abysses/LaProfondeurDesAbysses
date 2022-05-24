using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeControls : MonoBehaviour
{
    [Header("Liste Horizontal Box")]
    [Space]

    public List<Text> listHorizontalBox;

    public GameObject HorizontalBoxAChanger;

    public bool waitingForKey;

    Event keyEvent;
    KeyCode newKey;

    // Start is called before the first frame update
    void Start()
    {
        waitingForKey = false;
    }

    /*
     * Fonction pour changer un control
     * 
     * On appuie sur le bouton, on récupère l'horizontal box dans laquelle il se situe.
     * une fois fait on set la zone de texte vide et on attend qu'une touche du clavier soit pressé
     * on change ensuite la touche dans la bibliotheque de touche et dans la zone de texte 
     */
    public void InputChange(Button btn)
    {
        HorizontalBoxAChanger = btn.transform.parent.gameObject;

        GameObject txt = HorizontalBoxAChanger.GetComponentInChildren<Text>().gameObject;

        waitingForKey = true; 

    }

    private void OnGUI()
    {
        keyEvent = Event.current;
        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssigment(string keyName)
    {
        if(!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    IEnumerator WaitForKey()
    {
        while(!keyEvent.isKey)
        {
            yield return null;
        }
    }


    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();


        HorizontalBoxAChanger.transform.GetChild(1).GetComponent<Text>().text = keyName;
        //changer le json des touches.

        yield return null;

    }
}
