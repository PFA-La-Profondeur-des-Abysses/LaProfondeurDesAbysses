using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variable De Deplacement")]
    [Space]

    public float speed = 1f;

    public Vector2 velocity;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetAxis("Horizontal") > 0)
        {
            print("Droite");
            gameObject.transform.localScale.Set(1, 1, 1);
        }

        if(Input.GetAxis("Horizontal") < 0)
        {
            print("Gauche");
            gameObject.transform.localScale.Set(-1, 1, 1);
        }

        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rb.velocity = velocity;
        print(velocity);

    }
}
