using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class InputManager : MonoBehaviour
{

    public static InputManager IM;

    //deplacement personnage
    public KeyCode MoveUp { get; set; }
    public KeyCode MoveDown { get; set; }
    public KeyCode MoveRight { get; set; }
    public KeyCode MoveLeft { get; set; }

    //movement torche
    public KeyCode TorchUp { get; set; }
    public KeyCode TorchDown { get; set; }

    //deplacement bras
    public KeyCode ArmHorizontal { get; set; }
    public KeyCode ArmVertical { get; set; }



    void Awake()
    {
        if(IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if(IM!= this)
        {
            Destroy(gameObject);
        }
        //InputManager.CreateInputConfiguration("Keyboard");
      // InputManager.CreateDigitalAxis("Keyboard","Horizontal", KeyCode.Z, KeyCode.S, -3.00f, -3.00f);


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
