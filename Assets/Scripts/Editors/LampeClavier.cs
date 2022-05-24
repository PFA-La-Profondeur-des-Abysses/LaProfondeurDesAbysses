using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampeClavier : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {

       
        Vector3 rotationDestination = new Vector3(0, 0, 0) - transform.position;
        Vector3 rotation = Vector3.Lerp(transform.right, rotationDestination, Time.deltaTime * speed).normalized;
        transform.right = new Vector3(Mathf.Clamp(rotation.x, 0.45f, 1f), rotation.y, rotation.z);
        }
    }
}
