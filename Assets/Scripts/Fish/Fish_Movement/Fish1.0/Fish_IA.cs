using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_IA : MonoBehaviour
{
    FishSettings settings; //used to get fish settings 

    //flock mate = partenaire de troupeaux;
    //Steer = diriger

    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    // Variables we will have to update 
    [HideInInspector]
    public Vector3 averageFlockHeading;
    [HideInInspector]
    public Vector3 averageAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockMates;
    [HideInInspector]
    public int numPerceivedFlockMates = 2;

    Transform target;
    Transform cachedTransform; // other transform used to calculate positon 

    private void Awake()
    {
        cachedTransform = transform;
    }
    public void Initialize(FishSettings settings, Transform target)
    {
        this.target = target;
        this.settings = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
        numPerceivedFlockMates = 1;
}

    /*
     * Fonction used to update fish
     * 
     * 
     */
    public void UpdateFish()
    {
        Vector3 acceleration = Vector3.zero;

        if(target != null)
        {
            Vector3 offSetToTarget = (target.position - position);
            acceleration = SteerTowards(offSetToTarget) * settings.targetWeight;
        }

        //gerer le troupeaux (si il y en a 1)
        if (numPerceivedFlockMates != 0)
        {
            centreOfFlockMates /= numPerceivedFlockMates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockMates - position);

            var alignmentForce = SteerTowards(averageFlockHeading) * settings.alignWeight;
            var cohesionForce = SteerTowards(offsetToFlockmatesCentre) * settings.cohesionWeight;
            var seperationForce = SteerTowards(averageAvoidanceHeading) * settings.separateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        //gere la collision
        if (IsHeadingForCollision())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        //setup direction, vitesse -> le deplacement
        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        this.cachedTransform.position += velocity * Time.deltaTime;
        this.cachedTransform.forward = dir;
        this.position = cachedTransform.position;
        this.forward = dir;

    }


    /*
     * Fonction permettant de définir un vecteur3
     * etant le point où le "fish" doit de diriger.
     * 
     */
    Vector3 SteerTowards(Vector3 vector3) //se diriger vers
    {
        Vector3 nullZ = new Vector3(vector3.x, vector3.y, 0);
        vector3 = nullZ;
        Vector3 v = vector3.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, settings.maxSteerForce);
    }

    /*
     * Fonction using a sphere raycast to see if the fish
     * is going to collide something
     */
    bool IsHeadingForCollision()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, settings.avoidanceRadius, forward, settings.collisionAvoidDst, settings.obstacleMask, -Mathf.Infinity, Mathf.Infinity);

        if (hit)
        {
            return true;
        }
        else
        {

        }
        return false; // au cas où :)
    }


    /*
     * Fonction than "fire" a lot of raycast in a lot of direction 
     * used to find the obstacle and avoid this one.
     * 
     * 
     * 
     */
    Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = FishHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, settings.avoidanceRadius, dir, settings.collisionAvoidDst, settings.obstacleMask);

            if (!hit)
            {
                return dir;
            }
        }

        return forward;
    }
}
