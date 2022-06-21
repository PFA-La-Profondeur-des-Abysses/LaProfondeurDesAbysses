using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Destroyer : MonoBehaviour
{
    private Animator animator;
    public string action;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        ActivateText();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("player exit");
        if (!other.CompareTag("Player")) return;
        
        animator.SetTrigger("Off");
        PlayerMovement.player.LearnControl(action);
    }

    public void ActivateText()
    {
        animator.SetTrigger("On");
    }
}
