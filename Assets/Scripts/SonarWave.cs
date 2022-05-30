using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SonarWave : MonoBehaviour
{
    private Sonar sonar;
    void Awake()
    {
        sonar = transform.parent.GetComponent<Sonar>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interest"))
        {
            StartCoroutine(sonar.DetectObject(other.transform));
        }
    }
}
