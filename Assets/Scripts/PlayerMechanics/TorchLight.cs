using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{
    public KeyCode torchekey;
    public GameObject torche;
    public bool mouse = true; //variable permettant de savoir si le gestion de la lampe doit être faite au clavier ou à la souris
    public float speed; //vitesse de mouvement de la lampe
    public float angle; 
    public float axis;
    private Vector3 rotation;

    void Update()
    {
        if (Time.timeScale == 0) return;

        if(Input.GetKeyDown(torchekey)) torche.SetActive(!torche.activeSelf);
    }
    
    void FixedUpdate()
    {
        if (Time.timeScale == 0) return;
        
        if(!torche.activeSelf) return;
        
        if(mouse)
            MouseLight(); //lance la fonction gérant le mouvement de la lampe avec la souris
        else
            KeyboardLight(); //lance la fonction gérant le mouvement de la lampe avec le clavier
    }

    /*
     * Oriente la lampe dans la direction de la souris
     */
    private void MouseLight()
    {
        Vector2 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            //choppe les coordonnées de la souris par rapport à l'écran 
        Vector3 rotationDestination = new Vector3(worldpos.x, worldpos.y, 0) - transform.position; 
            //calcule l'angle de rotation jusqu'auquel la lampe devra tourner
        rotation = Vector3.Lerp(transform.right, rotationDestination, Time.deltaTime * speed).normalized;
            //calcule l'angle auquel la lampe doit être à cette frame (la lampe ne fait pas le chemin "rotation actuelle --> rotation voulue" instantanément
        transform.right = new Vector3(rotation.x, rotation.y, rotation.z);
            //assigne l'angle voulu à la lampe
    }

    /*
     * Tourne la lampe vers le bas ou le haut en fonction de la touche pressée
     */
    private void KeyboardLight()
    {
        if (Input.GetAxisRaw("VerticalLight") == 1f || Input.GetAxisRaw("VerticalLight") == -1f) 
            axis = Input.GetAxis("VerticalLight") / speed;
                //si une des touches de déplacement de la lampe est appuyée, donne à la variable axis la valeur de l'angle voulu
        else axis = Mathf.Lerp(axis, 0, Time.deltaTime * 4);
            //quand les touches n'est pas relachée, baisse graduellement la valeur d'axis (simule un inertie)
        angle = Mathf.Clamp(angle + axis, -30f, 30f);
        rotation = new Vector3(rotation.x, rotation.y, angle);
        transform.localRotation = Quaternion.Euler(rotation);
            //calcule la valeur de l'angle duquel la lampe doit bouger
    }
}
