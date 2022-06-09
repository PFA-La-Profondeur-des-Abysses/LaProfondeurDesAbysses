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

    float angleRotationZ;
    float angleRotationY;

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
        /*
         * Mouvement
         */
        _moveInput = playerControls.Player.Movement_Player.ReadValue<Vector2>();

        Vector2 movementDirection = new Vector2((float)_moveInput.x, (float)_moveInput.y);

        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        movementDirection.Normalize();

        rb.velocity = movementDirection * speed * Time.deltaTime;

        /*
        * Rotation
        */
        if (movementDirection != Vector2.zero)
        {
            bool canTurnRight = false;
            bool canTurnLeft = true;

            /*
             * Clamp la rotation à gauche : de -40 à -140
             * Clamp la rotation à droite : de 40 à 140
             * 
             * Faire la rota visuelle sur Y
             */

            /*
             * Rotation Gauche et droite
            */
            /*
            if(_moveInput.x < 0) // left gauche
            {
                if(canTurnLeft)
                {

                    if (transform.rotation.y == 180)
                    {

                        angleRotationY = Mathf.Clamp((_moveInput.x * 180 / 8) / rotationSpeed + angleRotationY, -180f, 0f);

                        Vector3 rotationYTo = new Vector3(transform.rotation.x, angleRotationY, transform.rotation.z );

                        transform.localRotation = Quaternion.Euler(rotationYTo);

                        canTurnLeft = false;
                        canTurnRight = true;
                    }
                    
                }
            }

            if(_moveInput.x > 0) // right droite 
            {
                if (canTurnRight)
                {


                    if (transform.rotation.y == 0)
                    {
                        angleRotationY = Mathf.Clamp((_moveInput.x * 180 / 8) / rotationSpeed + angleRotationY, -180, 0);

                        Vector3 rotationYTo = new Vector3(transform.rotation.x, angleRotationY, transform.rotation.z);

                        transform.localRotation = Quaternion.Euler(rotationYTo);

                        canTurnLeft = true;
                        canTurnRight = false;
                    }

                }
            }
            



            if (_moveInput.y != 0 && _moveInput.x > 0)
            {
                angleRotationZ = Mathf.Clamp((_moveInput.y*180/8) / rotationSpeed + angleRotationZ, -140f, -40f);

                Vector3 rotationTo = new Vector3(transform.rotation.x, transform.rotation.y, angleRotationZ);

                transform.localRotation = Quaternion.Euler(rotationTo);
            }

            if (_moveInput.y != 0 && _moveInput.x < 0)
            {
                angleRotationZ = Mathf.Clamp((_moveInput.y * 180 / 8) / rotationSpeed + angleRotationZ, -140f, -40f);

                Vector3 rotationTo = new Vector3(transform.rotation.x, transform.rotation.y, angleRotationZ);

                transform.localRotation = Quaternion.Euler(rotationTo);
            }
            */

            /*
            * ancien code 
            * 
            */


            Quaternion toRoration = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRoration, rotationSpeed * Time.deltaTime); //-40,-140 -> plage de valeur si on veut clamp.
            /*
            if (transform.rotation.z < 0f && transform.rotation.z > -180f)
            {
                Vector3 rotationTo = new Vector3(0, transform.rotation.y, transform.rotation.z);

                transform.localRotation = Quaternion.Euler(rotationTo);
            }

            else
            {


                Vector3 rotationTo = new Vector3(180, transform.rotation.y, transform.rotation.z);

                transform.localRotation = Quaternion.Euler(rotationTo);
            }
            */

        }

    }
}


