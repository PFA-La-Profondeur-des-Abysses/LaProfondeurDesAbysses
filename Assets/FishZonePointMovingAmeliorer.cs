using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishZonePointMovingAmeliorer : MonoBehaviour
{
    public Vector3 startPos;

    public float topPos;
    public float bottomPos;
    public float rightPos;
    public float leftPos;

    public List<Vector2> listPoints;

    public bool seachingNewPos;

    public Collider2D inThisCollider;

    public LayerMask layerZone;

    public Collider2D parentCollider;



    public void Start()
    {
        seachingNewPos = false;

        topPos = transform.parent.gameObject.GetComponent<PolygonCollider2D>().points[0].x ;
        bottomPos = transform.parent.gameObject.GetComponent<PolygonCollider2D>().points[0].y;
        rightPos = transform.parent.gameObject.GetComponent<PolygonCollider2D>().points[0].x;
        leftPos = transform.parent.gameObject.GetComponent<PolygonCollider2D>().points[0].x;

        foreach (Vector2 p in transform.parent.gameObject.GetComponent<PolygonCollider2D>().points)
        {

            listPoints.Add(p);
        }

        foreach (Vector2 v in listPoints)
        {
            if (v.x > leftPos)
            {
                leftPos = v.x;
            }

            if (v.x < rightPos)
            {
                rightPos = v.x;
            }

            if (v.y > topPos)
            {
                topPos = v.y;
            }
            if (v.y < bottomPos)
            {
                bottomPos = v.y;
            }
        }

        parentCollider = transform.GetComponentInParent<Collider2D>();

        startPos = transform.localPosition;

        newPos();

    }
    private void FixedUpdate()
    {

        if(seachingNewPos)
        {/*
            Vector2 pointPos = new Vector2(Random.Range(rightPos, leftPos), Random.Range(bottomPos, topPos));
            transform.localPosition = pointPos;

            collider = Physics2D.OverlapCircle(pointPos, 2f);

            if (collider != null && transform.GetComponentInParent<Collider2D>() == collider)
            {
                seachingNewPos = false;
            }*/

           // newPos();
        }


    }

    public void newPointPos()
    {
        seachingNewPos = true;
    }

    public void newPos()
    { 
        /*
        if(parentCollider != null)
        {
            Vector2[] tabVector = new Vector2[60];
            seachingNewPos = true;

            for (int i = 0; i < 60; i++)
            {
                tabVector[i] = new Vector2(Random.Range((float)rightPos, (float)leftPos), Random.Range((float)bottomPos, (float)topPos));

            }

            foreach (Vector2 v in tabVector)
            {
                //transform.localPosition = v;
                inThisCollider = Physics2D.OverlapCircle(v + (Vector2)transform.parent.position, 2f, layerZone);//test avec des pos local sur une world pos.
                                                                                                                //ClosestPoint
                if (inThisCollider != null && parentCollider == inThisCollider ) 
                {

                    //Debug.Log("New Pos ! " + v + " old : " + startPos);
                    transform.localPosition = v;
                    seachingNewPos = false;
                    //break;
                }
            }


            inThisCollider = Physics2D.OverlapCircle(transform.position, 3f, layerZone);

            if ((inThisCollider == null || parentCollider != inThisCollider || seachingNewPos == true) && parentCollider != null && tabVector.Length > 0)
            {

                transform.localPosition = parentCollider.ClosestPoint(tabVector[1]);

                parentCollider.OverlapPoint(transform.position);

                if (!parentCollider.OverlapPoint(transform.position)) // check si le point se situe dans le collider.
                {
                    transform.localPosition = startPos;
                }
                //transform.localPosition = startPos;

            }

            if (!parentCollider.OverlapPoint(transform.position) && parentCollider)
            {
                transform.localPosition = startPos;
            }

            tabVector = null;
        }
       */

        if(parentCollider != null)
        {
            Vector2[] tabVector = new Vector2[60];
            seachingNewPos = true;

            for (int i = 0; i < 60; i++)
            {
                tabVector[i] = new Vector2(Random.Range((float)rightPos, (float)leftPos), Random.Range((float)bottomPos, (float)topPos));

            }

            foreach (Vector2 v in tabVector)
            {
                //transform.localPosition = v;
               
                if (parentCollider.OverlapPoint(v + (Vector2)parentCollider.transform.position))
                {

                    //Debug.Log("New Pos ! " + v + " old : " + startPos);
                    transform.localPosition = v;
                    seachingNewPos = false;
                    break;
                }
            }

            if(!parentCollider.OverlapPoint(transform.position))
            {
                Vector3 oldpos = transform.position;
                transform.position = parentCollider.ClosestPoint(oldpos);
            }


        }
    }





    /*
     * Check Si l'object est dans le collider du parent.
     */
    public bool isIN(Vector2 v)
    {
        Debug.Log("Chekcing ");

        inThisCollider = Physics2D.OverlapCircle(v + (Vector2)transform.parent.position, 2f, layerZone);

        if (inThisCollider != null && transform.GetComponentInParent<Collider2D>() == inThisCollider)
        {
            Debug.Log("IT IS IN");
            return true;
        }
        else
        {

        }
        return false;
    }
}

