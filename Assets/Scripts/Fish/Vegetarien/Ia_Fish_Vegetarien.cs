using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_Fish_Vegetarien : IA_Fish
{

    // Start is called before the first frame update
    void Start()
    {
        getEaten = false;
        StartCoroutine(DetectingFood());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerHere)
        {
            return;
        }
        Moving();
    }
   
}
