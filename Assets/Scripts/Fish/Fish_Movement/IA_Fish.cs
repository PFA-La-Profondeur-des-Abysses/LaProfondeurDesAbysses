using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class IA_Fish : MonoBehaviour
{
    public IA_FishSettings settings; //used to get fish settings 

    //flock mate = partenaire de troupeaux;
    //Steer = diriger

    [Space]
    [Header("Fish Info :")]

    public FeedingRegime regime;
    public Transform food;
    public bool isEating;
    public bool isStoppedToEat;

    public float runToTargetSpeed = 2f;

    [Space]
    public float force;
    public float saveForce;
    public FishNames name;
    public bool playerHere;

    public GameObject fishZoneMovement;
    [Space]

    public bool getEaten;

    [Space]
    [HideInInspector]
    public Vector3 position;
    public Vector3 forward;
    public Vector3 right;
    public Vector3 velocity;
    private Transform head;


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

    [Space]
    [Header("Timer : ")]
    public float timerWaitDurationMax = 10;
    public float timerTargetDurationMax = 5;

    public float timerWaitUntilNextTarget;
    public float timerTarget;

    [Space]
    [Header("Spawner Info :")]
    //public GameObject spawnPointPrefab;
    //public GameObject spawnPoint;

    private Vector3 startScale;

    void Awake()
    {
        head = transform.GetChild(0);
        
        startScale = transform.localScale;
        Debug.Log(transform.rotation.eulerAngles);
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

        //spawnPoint = Instantiate(spawnPointPrefab, transform.position, transform.rotation);

        getEaten = false;
        saveForce = force;
    }



    /*
     * Fonction de deplacement  
     */
    public void Moving()
    {
        if (target && target == food && Vector3.Distance(target.position, head.position) < 0.1) isStoppedToEat = true;
        
        if (!getEaten || isStoppedToEat) // paralise le poisson si il se fait manger pour eviter des bugs
        {
            Vector3 acceleration = Vector3.zero;

            if (target == null)
            {
                target = fishZoneMovement.transform.GetChild(0);

            }

            GestionRotationFish();

            if (target != null)
            {

                if (isEating && food)
                {
                    if (Vector3.Distance(head.position, food.position) < 10f && target.gameObject.CompareTag("Fish"))
                    {

                        StartCoroutine(Eat());

                    }
                    else if (Vector3.Distance(head.position, food.position) < 1)
                    {

                        StartCoroutine(Eat());
                    }
                    else
                    {
                        Vector3 offSetToTarget = food.position - head.position;
                        acceleration = SteerTowards(offSetToTarget) * settings.targetWeight;
                    }
                }
                else if (Vector3.Distance(transform.position, target.position) < 10f)
                {
                    fishZoneMovement.transform.GetChild(0).GetComponent<FishZonePointMovingAmeliorer>().newPos();
                    target = fishZoneMovement.transform.GetChild(0);
                }
                else
                {
                    Vector3 offSetToTarget = target.position - position;
                    acceleration = SteerTowards(offSetToTarget) * settings.targetWeight;
                }


            }

            //gerer le banc de poisson (si il y en a 1)
            if (numPerceivedFlockMates != 0)
            {
                centreOfFlockMates = position;
                averageAvoidanceHeading = transform.right;
                averageFlockHeading = right;

                centreOfFlockMates /= numPerceivedFlockMates;

                Vector3 offsetToFlockmatesCentre = centreOfFlockMates - position;

                var alignmentForce = SteerTowards(averageFlockHeading) * settings.alignWeight;
                var cohesionForce = SteerTowards(offsetToFlockmatesCentre) * settings.cohesionWeight;
                var seperationForce = SteerTowards(averageAvoidanceHeading) * settings.separateWeight;

                acceleration += alignmentForce;
                acceleration += cohesionForce;
                acceleration += seperationForce;
            }

            //check si il y a une future collision et cherche une nouvelle trajectoire sans collision.
            if (isGoingToCollideSomething())
            {

                Vector3 collisionAvoidDir = ObstacleRays();
                Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settings.avoidCollisionWeight;
                acceleration += collisionAvoidForce;

            }
            //setup direction, vitesse -> le deplacement

            //Setup de la velocité, vitesse...


            if (isEating && food && Vector3.Distance(head.position, food.position) < 1f && !target.CompareTag("Fish") || isStoppedToEat)
            {

                StartCoroutine(Eat());

            }
            else
            {
                if (target != null)
                {
                    velocity += new Vector3(acceleration.x * Time.deltaTime, acceleration.y * Time.deltaTime, 0) * runToTargetSpeed;

                }
                else
                {
                    velocity += new Vector3(acceleration.x * Time.deltaTime, acceleration.y * Time.deltaTime, 0);
                }


                float speed = velocity.magnitude;

                Vector3 dir = velocity / speed;

                speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
                velocity = dir * speed;

                cachedTransform.position += velocity * Time.deltaTime;
                //this.cachedTransform.forward = dir;
                cachedTransform.right = dir;

                position = cachedTransform.position;

                //this.forward = dir;
                right = dir;
            }


        }

    }

    public void ChangementDirection()
    {
        if (food == null)
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
                        //target = spawnPoint.transform;
                        timerTarget = Random.Range(2, timerTargetDurationMax);

                    }
                }
            }
        }

    }

    /*
     * Gere la rotation du sprite du poisson
     * A REVOIR
     * 
     */
    public void GestionRotationFish()
    {

        if (right.x > 0)
        {

            transform.localScale = startScale;
        }

        if (right.x < 0)
        {

            transform.localScale = new Vector3(startScale.x, -startScale.y, startScale.z);
        }
    }

    /*
     * Fonction permettant de definir la direction (vector3)
     * vers laquelle le poisson va se diriger
     * 
     */
    Vector3 SteerTowards(Vector3 vector3)
    {

        Vector3 v = vector3.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, settings.maxSteerForce);
    }

    /*
     * Fonction lançant un raycast devant le poisson afin de detecter une collision
     *
     */
    public bool isGoingToCollideSomething()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, settings.avoidanceRadius, right, settings.collisionAvoidDst, settings.obstacleMask, -Mathf.Infinity, Mathf.Infinity);

       // Debug.DrawRay(transform.GetChild(0).transform.position, right * settings.collisionAvoidDst, Color.green);

        if (hit)
        {
            //Debug.DrawRay(transform.GetChild(0).transform.position, right * settings.collisionAvoidDst, Color.red);
            return true;
        }
        return false; // au cas où :)
    }


    /*
     * Fontion permettant de trouver la première direction que le poisson peut prendre
     * qui ne possède pas de collision à la fin
     * 
     */
    public Vector2 ObstacleRays()
    {

        Vector2[] rayDirection = IA_FishHelper();

        for (int i = 0; i < rayDirection.Length; i++)
        {
            Vector2 dir = cachedTransform.TransformDirection(rayDirection[i]);

            RaycastHit2D hit = Physics2D.CircleCast(transform.position, settings.avoidanceRadius, dir, settings.collisionAvoidDst, settings.obstacleMask);

            if (!hit)
            {
                return dir;
            }
        }

        return forward;
    }


    /*
     * Fonction permettant de calculer les direction des différents raycast a tirer afin d'éviter une collision.
     * 
     */
    public Vector2[] IA_FishHelper()
    {

        const int numViewDirections = 100;

        Vector2[] directions = new Vector2[numViewDirections];

        for (int i = 0; i < numViewDirections; i++)
        {
            float t = (float)i / numViewDirections;
            float inclination = Mathf.Acos(1 - 2 * t);

            float x = Mathf.Sin(inclination); //anciennemnt azimuth
            float y = Mathf.Cos(inclination); //anciennemnt azimuth


            directions[i] = new Vector3(x, y);


        }

        /*
         float turnFraction = 1.68f;
        for (int i = 0; i < numViewDirections; i++)
        {
            float dst =   i / (numViewDirections -1f);
            float angle  = 2 * Mathf.PI * turnFraction * i;
            float x = dst * Mathf.Sin(angle);
            float y = dst * Mathf.Cos(angle);
            directions[i] = new Vector3(x, y);
            Debug.DrawRay(transform.position, directions[i] *settings.collisionAvoidDst, Color.white);
        }
        */
        return directions;
    }

    public IEnumerator DetectingFood()
    {

        var particles = Physics2D.OverlapCircleAll(transform.position, 50, LayerMask.GetMask("Food"));
        if (particles.Length > 0)
        {
            foreach (var particule in particles)//.Where(particles => particles.gameObject.CompareTag(regime.ToString()))
            {
                if (particule.tag == regime.ToString() || regime.ToString() == FeedingRegime.Omnivore.ToString())
                {
                    food = particule.transform;
                    target = food;
                    isEating = true;
                    break;
                }
            }
        }
        else
        {
            isEating = false;
        }
        yield return new WaitForSeconds(0.1f); StartCoroutine(DetectingFood());
    }



    public IEnumerator Eat()
    {
        StopCoroutine(DetectingFood());
        GetComponent<Animator>().SetBool("Eating", true);
        float eatingTime = 1;
        while (eatingTime > 0 && food)
        {
            eatingTime -= Time.deltaTime;
            yield return null;
        }
        GetComponent<Animator>().SetBool("Eating", false);

        if (food)
        {
            Destroy(food.gameObject);
            target = null;
            food = null;
            isEating = false;
            isStoppedToEat = false;
        }

        yield return null;

    }

    void OnBecameVisible()
    {
        PlayerMovement.player.SeeFish(name);
        playerHere = true;

        force = saveForce;
    }

    private void OnBecameInvisible()
    {
        force = 99;
        playerHere = false;
    }


    /*
    public Transform NouvelleTarget()
    {
        float topPos = fishZoneMovement.transform.position.y + 5f;
        float bottomPos = fishZoneMovement.transform.position.y - 5f;
        float rightPos = fishZoneMovement.transform.position.x - 5f;
        float leftPos = fishZoneMovement.transform.position.x + 5f;
        foreach(Vector2 p in fishZoneMovement.GetComponent<PolygonCollider2D>().points)
        {
            if(p.x > leftPos)
            {
                leftPos = p.x;
            }
            if(p.x < rightPos)
            {
                rightPos = p.x;
            }
            if(p.y > topPos)
            {
                topPos = p.y;
            }
            if(p.y < bottomPos)
            {
                bottomPos = p.y;
            }
        }
        Vector2 pointPos = new Vector2(Random.Range(rightPos, leftPos), Random.Range(bottomPos, topPos));
           
   
        while(!fishZoneMovement.GetComponent<Collider2D>().OverlapPoint(pointPos))
        {
            pointPos = new Vector2(Random.Range(rightPos, leftPos), Random.Range(bottomPos, topPos));
        }
        fishZoneMovement.transform.GetChild(0).position = pointPos;
    
        return fishZoneMovement.transform.GetChild(0);
    }
    */
}