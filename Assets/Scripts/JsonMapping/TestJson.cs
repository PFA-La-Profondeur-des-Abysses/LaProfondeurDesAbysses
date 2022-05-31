using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using UnityEngine.InputSystem;

public class TestJson : MonoBehaviour
{
    public TextAsset json;
    public MappingList mappingList = new MappingList();

    // Start is called before the first frame update
    void Start()
    {

        mappingList = JsonUtility.FromJson<MappingList>(json.text);// Application.dataPath + "/Scripts/JSON/DataControlle.json");

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
