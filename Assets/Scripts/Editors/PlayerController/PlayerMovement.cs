using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variable De Deplacement")] [Space]

    public float startSpeed;
    public float speed = 7f;
    public float intertieSpeed;
    public float rotationSpeed = 2f;

    public float rotationLimit;

    public bool turbo;
    public float turboSpeed;

    public Vector2 velocity;

    public GameObject pivotRotation;
    
    public float angle; 
    public float axis;
    private Vector3 rotation;
    private int side;
    private float lookSide;
    private float lookDirection;

    float angleRotationZ;
    float angleRotationY;

    public bool canMove;
    private Rigidbody2D rb;

    public static PlayerMovement player;

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
        canMove = true;
        player = this;
        
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;

        mappingList = JsonUtility.FromJson<MappingList>(json.text);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //on check si les controles pour le deplacement sur l'axe horizontal sont press�s
        //On regarde ensuite la valeur : > 0 = on se d�place vers la droite
        //                               < 0 = on se d�place vers la gauche
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
        
        if(canMove)
        {
            speed = startSpeed;

            if (playerControls.Player.Turbo.IsPressed()) Turbo();

            MovementAndRotation();
        }
        

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

        rb.velocity = Vector2.Lerp(rb.velocity, movementDirection * speed, Time.deltaTime * intertieSpeed);

        /*
        * Rotation
        */
        bool canTurnRight = false;
        bool canTurnLeft = true;

        /*
         * Clamp la rotation � gauche : de -40 � -140
         * Clamp la rotation � droite : de 40 � 140
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
        
        float i = 1;

        if (_moveInput.y == 1f || _moveInput.y == -1f)
        {
            axis = movementDirection.y * rotationSpeed;
            i = _moveInput.y;
        }
        
        //si une des touches de déplacement de la lampe est appuyée, donne à la variable axis la valeur de l'angle voulu
        else axis = Mathf.Lerp(axis, 0, Time.deltaTime * rotationSpeed);
        //quand les touches n'est pas relachée, baisse graduellement la valeur d'axis (simule un inertie)
        angle = Mathf.Clamp(angle + axis, -rotationLimit, rotationLimit);
        var oldLookSide = lookSide;
        
        if (_moveInput.x == -1f || _moveInput.x == 1f)
        {
            //transform.localScale = new Vector3(_moveInput.x, 1, 1);
            //transform.DORotate(new Vector3(transform.rotation.y, _moveInput.x == -1 ? -180 : 0, transform.rotation.z), 0.3f);
            float angleDestination = 0;
            if (_moveInput.y == 1f || _moveInput.y == -1f) 
                angleDestination = Mathf.Clamp(angle, -30, 25);
            angle = Mathf.Lerp(angle, angleDestination, Time.deltaTime * rotationSpeed * 2);

            
            lookDirection = _moveInput.x == -1 ? -180f : 0f;
        }
        lookSide = Mathf.Lerp(oldLookSide, lookDirection, Time.deltaTime * 10);

        rotation = new Vector3(rotation.x, lookSide, angle * transform.localScale.x);
        transform.localRotation = Quaternion.Euler(rotation);

        //Quaternion toRoration = Quaternion.LookRotation(Vector3.forward, movementDirection);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRoration, rotationSpeed * Time.deltaTime); //-40,-140 -> plage de valeur si on veut clamp.
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

    public void Turbo()
    {
        speed = startSpeed * turboSpeed;
    }

    public void SeeFish(FishNames name)
    {
        Fish fish = Fish.GetFishFromName(name);

        if (fish.discovered) return; //EXIT si poisson déjà découvert
        
        fish.discovered = true;
        RapportManager.rapportManager.AddPage(fish);
    }

    public void TakePictureFish(FishNames name)
    {
        Fish fish = Fish.GetFishFromName(name);

        if (fish.pictureTaken) return;
        
        fish.pictureTaken = true;
        RapportManager.rapportManager.FillInfo(fish);
    }

    public void LearnControl(string action)
    {
        
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
}


