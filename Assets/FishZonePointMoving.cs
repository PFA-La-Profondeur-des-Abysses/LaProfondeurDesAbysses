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

    public void Start()
    {

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

        startPos = transform.position;

        newPointPos();
    }
    private void Update()
    {
        
    }
     
    public void newPointPos()
    {


        Vector3 oldPos = transform.localPosition;
        /*
        
        */


        Vector2 pointPos = new Vector2(Random.Range(rightPos, leftPos), Random.Range(bottomPos, topPos));



        transform.position = startPos;

        int i = 0;
        while(transform.parent.gameObject.GetComponent<Collider2D>().OverlapPoint(pointPos))
        {
            pointPos = new Vector2(Random.Range(rightPos, leftPos), Random.Range(bottomPos, topPos));
            i++;
            if(i>200)
            {
                break;
            }
        }

        Debug.Log(pointPos);
        transform.localPosition = pointPos;

        if (transform.position == oldPos)
        {
            transform.position = startPos;
        }


        

    }
}
