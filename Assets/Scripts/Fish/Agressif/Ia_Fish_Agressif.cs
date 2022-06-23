using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_Fish_Agressif : IA_Fish
{

    public float forceImpacte;

    public float timerImpact;
    public float waitAfterImpact = 2f;
    // Start is called before the first frame update
    void Start()
    {
        timerImpact = 0f;
        StartCoroutine(DetectingFood());
    }

    // Update is called once per frame
    void Update()
    {
        if(timerImpact < 0)
        {
            target = Cible();

            Moving();
        }
        else
        {
            timerImpact -= Time.deltaTime;
        }


    }

    /*
     * Fonction determinant la cible à attaquer.
     */
    public Transform Cible()
    {
        var listEnnemi = Physics2D.OverlapCircleAll(transform.position, 50, LayerMask.GetMask("Player"));
        
        Transform ennemiPos;

        if(listEnnemi.Length > 0)
        {
            ennemiPos = listEnnemi[0].gameObject.transform;
            foreach(Collider2D c in listEnnemi)
            {
                if(Vector3.Distance(c.gameObject.transform.position,transform.position) < Vector3.Distance(ennemiPos.position,transform.position))
                {
                    ennemiPos = c.gameObject.transform;
                }
            }

            return ennemiPos;
        }

        else
        {
            // a laisser merci 
        }
        return null;
    }


    /*
     * Lors de la collision avec la target, une force est ajouté a celle-ci pour la propulser dans la direction de deplacement
     * du poisson
     */
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!target.gameObject.CompareTag("food") && collision.gameObject == target.gameObject)
        {
            target.gameObject.GetComponent<Rigidbody2D>().AddForce(this.GetComponent<Rigidbody2D>().velocity * forceImpacte);
            target = null;
            timerImpact = waitAfterImpact;
        }
    }
}
