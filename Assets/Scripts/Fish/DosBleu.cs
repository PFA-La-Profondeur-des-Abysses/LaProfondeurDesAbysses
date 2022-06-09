using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DosBleu : MonoBehaviour
{
    public Transform fish;
    public Vector3 topLeftPos;
    public Vector3 botRightPos;
    public float speed;
    private Transform food;
    private bool eating;

    private Vector3 destination;

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

    private IEnumerator DetectFood()
    {
        var particles = Physics2D.OverlapCircleAll(transform.position, 50);
        Debug.Log(particles.Length);
        
        if(particles.Length > 0)
        {
            food = particles[0].transform;
            foreach (var particle in particles.Where(particle =>
                particle.CompareTag("Food") &&
                Vector2.Distance(transform.position, particle.transform.position) <
                Vector2.Distance(transform.position, food.position)))
            {
                Debug.Log("particule spot");
                food = particle.transform;
            }

            foreach (var particle in particles)
            {
                Debug.Log(particle.tag);
            }

            destination = food.position;
            //Debug.Log(destination);
        }
        
        yield return new WaitForSeconds(1);
        StartCoroutine(DetectFood());
    }

    // Update is called once per frame
    void Update()
    {
        if (fish.position == destination)
        {
            destination = new Vector3(Random.Range(topLeftPos.x, botRightPos.x),
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

    /*public void SpotFood(List<ParticleSystem.Particle> particles)
    {
        food = particles[0];
        foreach (var particle in particles.Where(particle => 
            Vector2.Distance(transform.position, particle.position) <
            Vector2.Distance(transform.position, food.position)))
        {
            food = particle;
        }
        destination = food.position;
    }

    public void LoseFood(List<ParticleSystem.Particle> particles)
    {
        foreach (var particle in particles.Where(particle => 
            particle.position == food.position &&
            Math.Abs(particle.rotation - food.rotation) < 0.01))
        {
            food = particle;
            destination = food.position;
        }
    }*/
}
