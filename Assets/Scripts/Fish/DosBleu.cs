using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DosBleu : MonoBehaviour
{
    public Transform fish;
    public Vector3 topLeftPos;
    public Vector3 botRightPos;
    public float speed;

    private Vector3 destination;

    void Awake()
    {
        topLeftPos = transform.GetChild(0).position;
        botRightPos = transform.GetChild(1).position;
        fish = transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (fish.position == destination)
        {
            destination = new Vector3(Random.Range(topLeftPos.x, botRightPos.x),
                Random.Range(botRightPos.y, topLeftPos.y), 0);
        }
        else
        {
            fish.right = fish.position - destination;
            if(fish.rotation.eulerAngles.z is > 90 and < 270)
            {
                fish.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                fish.localScale = new Vector3(1, 1, 1);
            }
            fish.position = Vector3.MoveTowards(fish.position, destination, Time.deltaTime * speed);
        }
    }
}
