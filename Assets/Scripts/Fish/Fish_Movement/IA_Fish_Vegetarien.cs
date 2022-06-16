using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Fish_Vegetarien : MonoBehaviour
{
    public IA_FishSettings settings; //used to get fish settings 

    //flock mate = partenaire de troupeaux;
    //Steer = diriger

    [HideInInspector]
    public Vector3 position;
   

    public Vector3 forward;
    public Vector3 right;
    public Vector3 velocity;

    // Variables we will have to update 
    [HideInInspector]
    public Vector3 averageFlockHeading;
    [HideInInspector]
    public Vector3 averageAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockMates;
    [HideInInspector]
    public int numPerceivedFlockMates;

    public Transform target;
    public Transform cachedTransform; // other transform used to calculate positon 


    public float timerWaitDurationMax = 10 ;
    public float timerTargetDurationMax = 5 ;

    public float timerWaitUntilNextTarget ;
    public float timerTarget;

    public GameObject spawnPointPrefab;
    public GameObject spawnPoint;

    void Awake()
    {
        forward = transform.forward;

        right = transform.right;

        cachedTransform = transform;

        numPerceivedFlockMates = 1;
        centreOfFlockMates = transform.position;
        Initialize(settings, null);

    }

    public void Initialize(IA_FishSettings settings, Transform target)
    {
        this.target = target;
        this.settings = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        right = cachedTransform.right;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;

        //velocity = transform.forward * startSpeed;
        velocity = transform.right * startSpeed;

        // averageAvoidanceHeading = transform.forward;
        averageAvoidanceHeading = transform.right;

        centreOfFlockMates = position;

        timerWaitUntilNextTarget = 5f;
        timerTarget = 0f;

        spawnPoint = Instantiate(spawnPointPrefab, transform.position, transform.rotation);
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (target == null || target.tag != "food")
        {
            if (target != null && timerWaitUntilNextTarget < 0)
            {

                timerTarget -= Time.deltaTime;
                if (timerTarget < 0)
                {
                    target = null;
                    timerWaitUntilNextTarget = Random.Range(2, timerWaitDurationMax);
                }
            }

            else
            {
                timerWaitUntilNextTarget -= Time.deltaTime;
                if (timerWaitUntilNextTarget < 0)
                {
                    target = spawnPoint.transform;
                    timerTarget = Random.Range(2, timerTargetDurationMax);

                }
            }
        }


        Moving();
       

    }

    void Moving()
    {
        Vector3 acceleration = Vector3.zero;

        GestionRotationFish();

        if (target != null)
        {
            Vector3 offSetToTarget = (target.position - position);

            acceleration = SteerTowards(offSetToTarget) * settings.targetWeight;
        }

    

        //gerer le troupeaux (si il y en a 1)
        if (numPerceivedFlockMates != 0)
        {
            centreOfFlockMates = position;
            averageAvoidanceHeading = transform.right;
            averageFlockHeading = right;

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
        if (isGoingToCollideSomething())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
            
        }

        //setup direction, vitesse -> le deplacement
   

        velocity += new Vector3(acceleration.x * Time.deltaTime,acceleration.y * Time.deltaTime,0);

        float speed = velocity.magnitude;

        Vector3 dir =  velocity / speed;

        speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        this.cachedTransform.position += velocity * Time.deltaTime;
        //this.cachedTransform.forward = dir;
        this.cachedTransform.right = dir;

        this.position = cachedTransform.position;

        //this.forward = dir;
        this.right = dir;

    }


    public void GestionRotationFish()
    {

        if (transform.rotation.z < 90 && transform.rotation.z > -90)
        {
            Debug.Log("positif y");
            this.transform.localScale = new Vector3(-0.34f, 0.34f, 0.34f);
        }

        if (transform.rotation.z > 90 || transform.rotation.z < -90)
        {
            Debug.Log("Negatif Y");
            this.transform.localScale = new Vector3(-0.34f, -0.34f, 0.34f);
        }
    }
    Vector3 SteerTowards(Vector3 vector3) //se diriger vers
    {
      

        Vector3 v = vector3.normalized * settings.maxSpeed - velocity;
        //Vector3 nullZ = new Vector3(v.x, v.y, 0);
        //v = nullZ;


        return Vector3.ClampMagnitude(v, settings.maxSteerForce);
    }


    bool isGoingToCollideSomething()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position,settings.avoidanceRadius, right, settings.collisionAvoidDst, settings.obstacleMask, -Mathf.Infinity,  Mathf.Infinity);

       
        if (hit)
        {
            Debug.DrawRay(transform.position, right * settings.collisionAvoidDst, Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, right * settings.collisionAvoidDst, Color.green);
        }
        return false; // au cas où :)
    }

    Vector2 ObstacleRays()
    {
        Vector2[] rayDirection = IA_FishHelper();

        for (int i = 0; i < rayDirection.Length; i++)
        {
            Vector2 dir = cachedTransform.TransformDirection(rayDirection[i]);

            RaycastHit2D hit = Physics2D.CircleCast(transform.position, settings.avoidanceRadius, dir, settings.collisionAvoidDst, settings.obstacleMask);

            Debug.DrawRay(transform.position, dir, Color.gray);
            if (!hit)
            {
                return dir;
            }
        }

        return forward;
    }



    public Vector2[] IA_FishHelper()
    {
        const int numViewDirections = 300;

        Vector2[] directions = new Vector2[numViewDirections];

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
}
