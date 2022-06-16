using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishSettings : ScriptableObject
{
    //Fish Setting

    public float minSpeed = 2;
    public float maxSpeed = 5;

    public float perceptionRadius = 2.5f;

    //variable permettant de setup le radius du cone dans lequel le poisson essaiera d'éviter les obstacles qui s'y trouve 
    public float avoidanceRadius = 1;

    //permet de simuler une force dans les virages des poissons.
    public float maxSteerForce = 3;

    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float separateWeight = 1;

    public float targetWeight = 1;


    [Header("Collisions")]
    public LayerMask obstacleMask;
    public float boundsRadius = .27f;
    public float avoidCollisionWeight = 10;
    public float collisionAvoidDst = 5;

}
