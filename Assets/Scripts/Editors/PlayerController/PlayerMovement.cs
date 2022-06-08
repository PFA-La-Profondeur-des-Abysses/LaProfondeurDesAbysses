using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variable De Deplacement")]
    [Space]

    public float speed = 7f;

    public float rotationSpeed = 2f;

    public Vector2 velocity;

    public GameObject pivotRotation;



    private Rigidbody2D rb;

    [Space]
    [Header("Gestion Controlle Mapping ")]
    [Space]

    public TextAsset json;
    public MappingList mappingList = new MappingList();

    [Space]
    [Header("Gestion Controlle Mapping ")]
    [Space]


    private PlayerControls playerControls;

    private Vector2 _moveInput;

    private Vector2 _armInput;

    private bool _torchInput;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;

        mappingList = JsonUtility.FromJson<MappingList>(json.text);

    }

    // Update is called once per frame
    void Update()
    {
        //on check si les controles pour le deplacement sur l'axe horizontal sont pressés
        //On regarde ensuite la valeur : > 0 = on se déplace vers la droite
        //                               < 0 = on se déplace vers la gauche
        /*
        _moveInput = playerControls.Player.Movement_Player.ReadValue<Vector2>();


        velocity = new Vector2((float)_moveInput.x, (float)_moveInput.y);

        float inputMagnitude = Mathf.Clamp01(velocity.magnitude);

        velocity.Normalize();

        rb.velocity = velocity * speed * Time.deltaTime;


        if(velocity != Vector2.zero)
        {                
            Quaternion toRoration = Quaternion.LookRotation(Vector3.forward, velocity);

        }
        */

        MovementAndRotation();

    }


    [Serializable]
    public class Mapping
    {
        public string nomControlle;
        public KeyCode keyCode;
    }


    [Serializable]
    public class MappingList
    {
        public Mapping[] mapping;


    }


    public void MovementAndRotation()
    {
        _moveInput = playerControls.Player.Movement_Player.ReadValue<Vector2>();


        Vector2 movementDirection = new Vector2((float)_moveInput.x, (float)_moveInput.y);

        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        movementDirection.Normalize();

        rb.velocity = movementDirection * speed * Time.deltaTime;


        if (movementDirection != Vector2.zero)
        {
            /*
             * Clamp la rotation à gauche : de -40 à -140
             * Clamp la rotation à droite : de 40 à 140
             * 
             */

            if(_moveInput.x != 0)
            {
                if(_moveInput.x < 0) // gauche
                {

                }

                if(_moveInput.x > 0) // droite 
                {

                }
            }



            // Code effecuant la rotation en fonction de la direction où le player regarde.
            Quaternion toRoration = Quaternion.LookRotation(Vector3.forward, movementDirection);
            Debug.Log(toRoration);

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRoration, rotationSpeed * Time.deltaTime); //-40,-140 -> plage de valeur si on veut clamp.

            

        }

    }
}


