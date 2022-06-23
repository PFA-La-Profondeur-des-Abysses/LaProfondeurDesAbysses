using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_Fish_Carnivore : IA_Fish
{
    public float timerCarnivore =0f;
    public float timerDuraction = 60f;

    public Transform proiePos = null;

    public Component scriptProie;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DetectingFood());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!playerHere)
        {
            timerCarnivore = 5f;
            return;
        }

        Moving();


        if (timerCarnivore <= 0 && !isEating)
        {
            ChercheCible();
        }
        else
        {
            timerCarnivore -= Time.deltaTime;
        }

        if (target == null)
        {
            this.isStoppedToEat = false;
        }

        if (target != null && Vector3.Distance(transform.position, target.position) < 6f)
        {


            if (target.gameObject.GetComponent<Ia_Fish_Vegetarien>())
            {
                Debug.Log("Il est vegan le con");
                this.isStoppedToEat = true;
                proiePos.gameObject.GetComponent<Ia_Fish_Vegetarien>().getEaten = true;
            }

            if (target.gameObject.GetComponent<Ia_Fish_Fuillard>())
            {
                target.gameObject.GetComponent<Ia_Fish_Fuillard>().getEaten = true;
            }
        }


    }

    /*
     * Fonction determinant la cible que le poisson va aller manger (un autre poisson proche).
     */
    public void ChercheCible()
    {
        if(food == null && !isEating)
        {
            Collider2D[] listProie = Physics2D.OverlapCircleAll(transform.position, 75, LayerMask.GetMask("Fish"));
            
            proiePos = null;

            if (listProie.Length > 1)
            {
                for(int i = 0; i< listProie.Length;i++)
                {
                    if(listProie[i] != GetComponentInChildren<Collider2D>() && (listProie[i].gameObject.transform.parent.GetComponent<Ia_Fish_Vegetarien>() || listProie[i].gameObject.transform.parent.GetComponent<Ia_Fish_Fuillard>()))
                    {
                        proiePos = listProie[i].gameObject.transform.parent.transform ; // on renvoie le transform du parent
                    }
                }

                if (proiePos != null)
                {
                    if (proiePos.gameObject.GetComponent<Ia_Fish_Vegetarien>())
                    {
                        if (proiePos.gameObject.GetComponent<Ia_Fish_Vegetarien>().force < this.force)
                        { 

                            isEating = true;
                            food = proiePos;
                            target = food;
                            timerCarnivore = timerDuraction;
                      
                        }

                    }
                    if (proiePos.gameObject.GetComponent<Ia_Fish_Fuillard>())
                    {
                        if (proiePos.gameObject.GetComponent<Ia_Fish_Fuillard>().force < this.force)
                        {

                            isEating = true;
                            food = proiePos;
                            target = food;
                            timerCarnivore = timerDuraction;
      
                        }

                    }
                }

               
            }


        }
     

    }


}
