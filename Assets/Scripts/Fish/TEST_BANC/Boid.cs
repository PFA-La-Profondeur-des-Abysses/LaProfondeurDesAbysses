using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    BoidSettings settings;

    // State

    public Vector3 position;

    public Vector3 right;
    public Vector3 forward;
    public Vector3 velocity;

    // To update:
    public Vector3 acceleration;

    public Vector3 avgFlockHeading;

    public Vector3 avgAvoidanceHeading;

    public Vector3 centreOfFlockmates;

    public int numPerceivedFlockmates;

    // Cached

    public Transform cachedTransform;
    public Transform target;


    public GameObject canonRaycast;
    void Awake () {

        cachedTransform = transform;
        forward = transform.forward;
    }

    public void Initialize (BoidSettings settings, Transform target) {
        this.target = target;
        this.settings = settings;

        position = cachedTransform.position;
        right = cachedTransform.right;

        forward = cachedTransform.forward;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = transform.right * startSpeed;
        numPerceivedFlockmates = 1;
        centreOfFlockmates = transform.position;
    }



    public void UpdateBoid () {
        Vector3 acceleration = Vector3.zero;

        if (target != null) {
            Vector3 offsetToTarget = (target.position - position);
            acceleration = SteerTowards (offsetToTarget) * settings.targetWeight;
        }

        if (numPerceivedFlockmates != 0) {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards (avgFlockHeading) * settings.alignWeight;
            var cohesionForce = SteerTowards (offsetToFlockmatesCentre) * settings.cohesionWeight;
            var seperationForce = SteerTowards (avgAvoidanceHeading) * settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision ()) {
            Vector3 collisionAvoidDir = ObstacleRays ();
            Vector3 collisionAvoidForce = SteerTowards (collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        //Setup de la velocité, vitesse...
        velocity += new Vector3(acceleration.x * Time.deltaTime, acceleration.y * Time.deltaTime, 0);

        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp (speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        this.cachedTransform.position += velocity * Time.deltaTime;
        this.cachedTransform.right = dir;
        this.position = cachedTransform.position;
        this.right = dir;
    }

    public  bool IsHeadingForCollision () 
    {

        RaycastHit2D[] hit = Physics2D.CircleCastAll(canonRaycast.transform.position, settings.avoidanceRadius, right, settings.collisionAvoidDst, settings.obstacleMask);

        Debug.DrawRay(canonRaycast.transform.position, right * settings.collisionAvoidDst, Color.green);

        if (hit.Length > 1)
        {
            Debug.Log("Collision Avec : " + hit[1].collider.gameObject.name);
            Debug.DrawRay(canonRaycast.transform.position, right * settings.collisionAvoidDst, Color.red);
            return true;
        
        }
        /*
        if (hit)
        {
            Debug.Log("Collision Avec : " + hit.collider.gameObject.name);
            Debug.DrawRay(canonRaycast.transform.position, right * settings.collisionAvoidDst, Color.red);
            return true;
        }*/
        else
        {

        }
        return false; // au cas où :)
    }

    void OnSceneGUI()
    {
        UnityEditor.Handles.DrawWireDisc(GetComponent<Collider>().transform.position, Vector3.back, settings.collisionAvoidDst);
    }


    Vector3 ObstacleRays () {

        Vector3[] rayDirections = BoidHelper();
        
        Debug.Log("Bug ici : " + rayDirections);

        for (int i = 0; i < rayDirections.Length; i++) 
        {
            Vector2 dir = cachedTransform.TransformDirection (rayDirections[i]);

            RaycastHit2D[] hit = Physics2D.CircleCastAll(canonRaycast.transform.position, settings.avoidanceRadius, dir, settings.collisionAvoidDst, settings.obstacleMask);

            if(hit.Length < 3)
            {
                if(hit.Length < 2)
                {
                    return dir;
                }
                

                if(hit.Length == 2 && hit[1].collider.gameObject.CompareTag("Fish"))
                {
                    return dir;
                }
            }
            
            
            /*
            if (hit.collider.gameObject != this.transform.parent.gameObject)
            {
                if (!hit)
                {
                    return dir;
                }
            }*/
        }

        return forward;
    }

    public Vector3[] BoidHelper()
    {
        const int numViewDirections = 300;

        Vector3[] directions = new Vector3[numViewDirections];

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < numViewDirections; i++)
        {
            float t = (float)i / numViewDirections;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination); //anciennemnt azimuth
            float y = Mathf.Cos(inclination); //anciennemnt azimuth


            directions[i] = new Vector3(x, y);


        }
        return directions;
    }
    Vector3 SteerTowards (Vector3 vector) 
    {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude (v, settings.maxSteerForce);
    }

}