using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 7; 
    public float accel = 12;
    public bool acc;
    public float zoom = 0;
    Camera cam;


    public void Start()
    {
        cam = GetComponent<Camera>();
    }
    public void Update ()
    {
        float vit = 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            vit = accel;
            acc = true;
        }
        else
        {
            vit = speed;
            acc = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            zoom += Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.E))
        {
            zoom -= Time.deltaTime * 10;
        }

        gameObject.transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * vit *Time.deltaTime);
    }

    public void LateUpdate()
    {
        zoom = Mathf.Clamp(zoom, 0f, 60f);
        cam.fieldOfView = 60 + zoom;
    }
}
