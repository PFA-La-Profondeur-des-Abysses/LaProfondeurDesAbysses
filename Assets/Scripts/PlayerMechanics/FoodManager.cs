using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    private GameObject meat;
    private GameObject leaf;
    private GameObject food;

    private void Start()
    {
        leaf = transform.GetChild(0).gameObject;
        meat = transform.GetChild(1).gameObject;
        food = leaf;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) ChangeType();
        if(Input.GetKeyDown(KeyCode.F)) ThrowFood();
    }

    private void ThrowFood()
    {
        var newFood = Instantiate(food, transform.position, Quaternion.identity);
        newFood.SetActive(true);
        newFood.GetComponent<ParticleSystem>().Play();
    }

    private void ChangeType()
    {
        food = food == leaf ? meat : leaf;
    }
}
