using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{

    public bool mouse = true;
    public float speed;

    private Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mouse)
        {
            Vector2 worldpos =
                Camera.main.ScreenToWorldPoint(Input
                    .mousePosition); //choppe les coordonnées de la souris par rapport à l'écran 
            Vector3 rotationDestination = new Vector3(worldpos.x, worldpos.y, 0) - transform.position;
            rotation = Vector3.Lerp(transform.right, rotationDestination, Time.deltaTime * speed).normalized;
        }
        else
        {
            rotation = new Vector3(0, 0, rotation.z + Input.GetAxis("Vertical"));
        }
        transform.right = new Vector3(Mathf.Clamp(rotation.x, 0.45f, 1f), rotation.y, rotation.z);
    }
}
