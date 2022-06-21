using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class DosBleu : MonoBehaviour
{
    public Transform fish;
    public Vector3 topLeftPos;
    public Vector3 botRightPos;
    public float speed;
    private Transform food;
    private bool foodDetected;
    private bool eating;
    public FeedingRegime regime;

    private Vector3 destination;

    public FishNames name;

    void Awake()
    {
        topLeftPos = transform.GetChild(0).position;
        botRightPos = transform.GetChild(1).position;
        fish = transform.GetChild(2);
    }
    
    void Start()
    {
        StartCoroutine(DetectFood());
    }

    // Update is called once per frame
    void Update()
    {
        if (fish.position == destination)
        {
            if (eating) StartCoroutine(Eat());
            else destination = new Vector3(Random.Range(topLeftPos.x, botRightPos.x),
                Random.Range(botRightPos.y, topLeftPos.y), 0);
        }
        else
        {
            fish.right = fish.position - destination;
            if(fish.rotation.eulerAngles.z is > 90 and < 270)
            {
                fish.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                fish.localScale = new Vector3(1, 1, 1);
            }
            fish.position = Vector3.MoveTowards(fish.position, destination, Time.deltaTime * speed);
        }
    }

    private IEnumerator DetectFood()
    {
        var particles = Physics2D.OverlapCircleAll(transform.position, 50, LayerMask.GetMask("Food"));
        
        if(particles.Length > 0)
        {
            var foundFood = false;
            food = particles[particles.Length - 1].transform;
            foreach (var particle in particles.Where(particle =>
                (regime == FeedingRegime.Omnivore || particle.gameObject.CompareTag(regime.ToString()))))
            {
                food = particle.transform;
                foundFood = true;
                break;
            }
            
            if(foundFood)
            {
                destination = food.position;
                eating = true;
            }
        }
        else eating = false;

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DetectFood());
    }

    private IEnumerator Eat()
    {
        StopCoroutine(DetectFood());
        
        float eatingTime = 1;
        while (eatingTime > 0 && food)
        {
            eatingTime -= Time.deltaTime;
            yield return null;
        }
        
        if(food) Destroy(food.gameObject);
        eating = false;

        yield return null;
        
        StartCoroutine(DetectFood());
        destination = transform.position;
    }

    void OnBecameVisible()
    {
        PlayerMovement.player.SeeFish(name);
    }
}
