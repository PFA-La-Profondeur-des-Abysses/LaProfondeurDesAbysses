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
        //on check si les controles pour le deplacement sur l'axe horizontal sont pressés
        //On regarde ensuite la valeur : > 0 = on se déplace vers la droite
        //                               < 0 = on se déplace vers la gauche
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

        //une fois les valeurs récupérés on crée une velocité avec qu'on applique au player 
        velocity = new Vector2( (float)(Input.GetAxis("Horizontal") * 2f) , (float)(Input.GetAxis("Vertical") * 2f));

        rb.velocity = velocity;

    }
}
