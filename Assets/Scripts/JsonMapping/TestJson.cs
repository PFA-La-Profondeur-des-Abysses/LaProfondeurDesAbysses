using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ControllerData controllerData = new ControllerData();
        controllerData.nomControlle = "Gauche";
        controllerData.keyCode = KeyCode.A;

        string json = JsonUtility.ToJson(controllerData);

        Debug.Log(json);

        File.WriteAllText(Application.dataPath + "/Scripts/JSON/DataControlle.json", json);
    }




    private class ControllerData
    {
        public string nomControlle;
        public KeyCode keyCode;
    }
}
