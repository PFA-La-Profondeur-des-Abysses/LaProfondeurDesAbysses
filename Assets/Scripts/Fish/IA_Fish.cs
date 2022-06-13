using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Fish : MonoBehaviour
{
    public int niveauForce; // int permetant de classer les poisons, un poisson carnivore ne s'attaquera qu'a des poissons ayant un niveau plus faible 

    public Vector3 destination;

    public Vector3 botRightPos;
    public Vector3 botLeftPos;

    public float speed;

    public float faster;



    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 
    void Update()
    {
        
    }


    public void Deplacement()
    {
        if (transform.position == destination)
        {
            destination = new Vector3(Random.Range(botLeftPos.x, botRightPos.x),
                Random.Range(botRightPos.y, botLeftPos.y), 0);
        }
        else
        {
            transform.right = transform.position - destination;
            if (transform.rotation.eulerAngles.z is > 90 and < 270)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        }
    }

    

}
