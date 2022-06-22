using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_Fish_Carnivore : IA_Fish
{
    public float timerCarnivore =0f;
    public float timerDuraction = 60f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timerCarnivore < 0)
        {
            target = Cible();
        }

        timerCarnivore -= Time.deltaTime;

        Moving();
    }

    /*
     * Fonction determinant la cible que le poisson va aller manger (un autre poisson proche).
     */
    public Transform Cible()
    {
        var listProie = Physics2D.OverlapCircleAll(transform.position, 50, LayerMask.GetMask("Fish"));
        Transform proiePos;

        if(listProie.Length > 0)
        {
            proiePos = listProie[0].gameObject.transform;

            foreach(Collider2D c in listProie)
            {
                if(Vector3.Distance(proiePos.position,this.position) > Vector3.Distance(c.gameObject.transform.position,this.position) )
                {
                    proiePos = c.gameObject.transform;
                }
            }

            isEating = true;
            food = proiePos;
            timerCarnivore = timerDuraction;
            return proiePos;
        }


        return null;
    }
}
