using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_Fish_Fuillard : IA_Fish
{
    public List<GameObject> PointDeFuite;
    public bool cach�;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DetectingFood());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!cach�)
        {
            Fuite();
            Moving();
            if(target.gameObject.CompareTag("Cachette"))
            {
                if (Vector3.Distance(transform.position, target.gameObject.transform.position) < 3)
                {
                    cach� = true;
                }
            }
        }

        if (cach�)
        {
            var objectAFuir = Physics2D.OverlapCircleAll(transform.position, 30, LayerMask.GetMask("Player"));
            if (objectAFuir.Length > 0)
            {
                return;
            }
            else
            {
                cach� = false;
                target = spawnPoint.transform;
            }
        }
    }

    /*
     * Fonction cherchant dans la liste de point de fuite, le point de fuite le plus �loign� 
     * du danger que le poisson doit fuire et le rejoindre au plus vite 
     */
    public void Fuite()
    {
        GameObject lePlusProche;
        if(doitFuir())
        {
            var objectAFuir = Physics2D.OverlapCircleAll(transform.position, 30, LayerMask.GetMask("Player"));
            if (objectAFuir.Length > 0)
            {
                lePlusProche = objectAFuir[0].gameObject;
                foreach (Collider2D c in objectAFuir)
                {
                    if (Vector3.Distance(lePlusProche.transform.position, transform.position) < Vector3.Distance(c.gameObject.transform.position, transform.position))
                    {
                        lePlusProche = c.gameObject;
                    }
                }


                GameObject pointFuiteChoisi = PointDeFuite[0];

                foreach (GameObject go in PointDeFuite)
                {
                    if (Vector3.Distance(go.transform.position, transform.position) < Vector3.Distance(lePlusProche.transform.position, go.transform.position)
                        && Vector3.Distance(go.transform.position, transform.position) < Vector3.Distance(pointFuiteChoisi.transform.position, transform.position))

                    {
                        pointFuiteChoisi = go;
                    }
                }


                target = pointFuiteChoisi.transform;
                food = null;
                isEating = false;

            }
           
        }
    }

    /*
     * Fonction qui d�fini si oui ou non le poisson doit fult 
     */ 
    public bool doitFuir() 
    {
        var objectAFuir = Physics2D.OverlapCircleAll(transform.position, 30, LayerMask.GetMask("Player"));
        if (objectAFuir.Length > 0)
        {
            return true;
        }
        else
        {

        }
        return false;
    }





}
