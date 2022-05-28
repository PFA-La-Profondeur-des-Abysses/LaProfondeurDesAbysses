using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    [SerializeField] private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame

    /* 
     * Si la cam�ra poss�de une Target
     * On r�cup�re la positon de la target par rapport � la camera (WorldToViewportPoint)
     * Ensuite on calcule un delta qui est la position de la target moin la position de la camera (dans le monde).
     * On calcule ensuite la destionation de la cam�ra
     * Et on utilise la fonction SmoothDamp pour smooth le deplacement de la cam�ra jusqu'� sa destination 
     * 
     * 
     */
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}