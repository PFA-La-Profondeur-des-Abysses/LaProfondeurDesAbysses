using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class TestJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
   
        List<ControllerData> Mapping = FileHandler.ReadListFromJSON<ControllerData>(Application.dataPath + "/Scripts/JSON/DataControlle.json");

        Debug.Log(Mapping.Count);

    }




    private class ControllerData
    {
        public string nomControlle;
        public KeyCode keyCode;
    }
}
