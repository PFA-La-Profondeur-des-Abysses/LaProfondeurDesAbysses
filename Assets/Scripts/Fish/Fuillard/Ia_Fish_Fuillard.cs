using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_Fish_Fuillard : IA_Fish
{
    public List<GameObject> PointDeFuite;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DetectingFood());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Fuite();
        Moving();

        if(target !=null)
        {
            if(transform == target)
            {
                target = null;
            }
        }
    }
    /*
     * Fonction cherchant dans la liste de point de fuite, le point de fuite le plus éloigné 
     * du danger que le poisson doit fuire et le rejoindre au plus vite 
     */
    public void Fuite()
    {
        GameObject lePlusProche;
        if(doitFuir())
        {
            var objectAFuir = Physics2D.OverlapCircleAll(transform.position, 50, LayerMask.GetMask("Fish", "Player"));
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
                    if (Vector3.Distance(go.transform.position, transform.position) < Vector3.Distance(lePlusProche.transform.position, transform.position)
                        && Vector3.Distance(go.transform.position, transform.position) < Vector3.Distance(pointFuiteChoisi.transform.position, transform.position))

                    {
                        pointFuiteChoisi = go;
                    }
                }


                target = pointFuiteChoisi.transform;
            }
           
        }
    }
    /*
     * Fonction qui défini si oui ou non le poisson doit fult 
     */ 
    public bool doitFuir() 
    {
        var objectAFuir = Physics2D.OverlapCircleAll(transform.position, 50, LayerMask.GetMask("Fish","Player"));
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
