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

    public Vector2 velocity;



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

        _moveInput = playerControls.Player.Movement_Player.ReadValue<Vector2>();


        if (_moveInput.x > 0)
        {
            if (gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.DORotate(new Vector2(0, 0), 0.5f);
            }

        }

        if (_moveInput.x < 0)
        {
            if (gameObject.transform.rotation.y != 180)
            {
                gameObject.transform.DORotate(new Vector2(0, 180), 0.5f);
            }
        }

        speed = 1f;
        velocity = new Vector2((float)_moveInput.x * speed, (float)_moveInput.y * speed) ;

        rb.velocity = velocity;

        

        print(transform.forward);

        transform.LookAt(new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y));


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

    public void OnMovement_player()
    {
        //Mouvement_Player;
    }
}


