using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishZonePointMoving : MonoBehaviour
{
    public Vector3 startPos;
    public float topPos;

    public float bottomPos;


    public float rightPos;

    public float leftPos;
    public List<Vector2> listPoints;

    public bool seachingNewPos;
    public Collider2D collider;

    public void Start()
    {
        seachingNewPos = false;
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

        startPos = transform.localPosition;





        newPointPos();
    }
    private void FixedUpdate()
    {
        if (seachingNewPos)
        {
            Vector2 pointPos = new Vector2(Random.Range(rightPos, leftPos), Random.Range(bottomPos, topPos));
            transform.localPosition = pointPos;

            collider = Physics2D.OverlapCircle(pointPos, 2f);

            if (collider != null && transform.GetComponentInParent<Collider2D>() == collider)
            {
                seachingNewPos = false;
            }
        }
    }

    public void newPointPos()
    {
        seachingNewPos = true;
    }

    /*
     * Check Si l'object est dans le collider du parent.
     */
    public bool isIN(Vector2 v)
    {
        Debug.Log("Chekcing ");

        collider = Physics2D.OverlapCircle(v, 2f);

        if (collider != null && transform.GetComponentInParent<Collider2D>() == collider)
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

