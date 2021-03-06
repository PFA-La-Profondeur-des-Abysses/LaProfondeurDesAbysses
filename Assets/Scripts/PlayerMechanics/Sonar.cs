using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Sonar : MonoBehaviour
{
    public List<Transform> interestPoints;

    public Transform scan;
    public Transform spotter;
    new public Transform light;
    public float speed;

    private List<GameObject> lights = new ();
    private float scanningTime;
    private Transform player;

    private AudioSource sfx;

    public List<Sprite> sprites;
    public List<Color> lightColors = new ();

    void Start()
    {
        player = transform.GetComponentInParent<PlayerMovement>().transform;
        sfx = GetComponent<AudioSource>();
    }

    /*
     * A chaque frame:
     * vérifie si le joueur veut scanner, puis si un scan est déjà en cours
     * désactive le cercle du sonnar quand celui-ci arrive à expiration
     */
    void Update()
    {
        if (Time.timeScale == 0) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!scan.gameObject.activeSelf)
            {
                DeactivateSpotter();
                StartCoroutine(Scan());
            }
        }

        if (scanningTime > 0)
        {
            scanningTime -= Time.deltaTime;
        }
        else
        {
            if (!spotter.gameObject.activeSelf) return;
            DeactivateSpotter();
        }
        
        spotter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        transform.localScale = player.localScale;
    }

    /*
     * Lance un scan
     * Le scan grandit au fur et à mesure du temps, et l'objet du scan détecte les objets d'intérêt lui même
     * (donc pas dans ce code)
     */
    private IEnumerator Scan()
    {
        sfx.Play();
        
        scan.gameObject.SetActive(true);    
        scan.localScale = new Vector3(1, 1, 1);
        scan.position = transform.position;
        scan.rotation = Quaternion.identity;
        scan.parent = transform.root.parent;
        float t = 0;
        while (t < 3)
        {
            float coordinate = t * speed;
            scan.localScale = new Vector3(coordinate, coordinate, 1);
            t += Time.deltaTime;
            scanningTime = 3f;
            yield return null;
        }
        StartCoroutine(ActivateCircle());
        scan.gameObject.SetActive(false);
    }
    
    /*
     * Lorsque l'objet du scan détecte un objet, il lance cette fonction
     * La fonction active le cercle du sonnar, puis affiche l'objet détecté par une lumière
     */
    public void DetectObject(GameObject target)
    {
        StartCoroutine(ActivateCircle());
        StartCoroutine(InstantiateTracker(target));
    }

    public IEnumerator InstantiateTracker(GameObject target)
    {
        GameObject newLight = Instantiate(light.gameObject, spotter);
        newLight.SetActive(true);
        
        StartCoroutine(FollowTarget(newLight, target));

        int i = GetSonarType(target);
        
        newLight.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[i];
        newLight.transform.GetChild(0).GetComponent<SpriteRenderer>().color = lightColors[i];
        newLight.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2 + i;
        
        lights.Add(newLight);
        float t = 0;
        while (t < 1f)
        {
            if (!newLight) yield break;
            float coordinate = t * 4f;
            newLight.transform.GetChild(0).localPosition = new Vector3(coordinate, 0, 0);
            t += Time.deltaTime;
            scanningTime = 3f;
            yield return null;
        }
        if(newLight) newLight.transform.GetChild(0).localPosition = new Vector3(4, 0, 0);
    }
    
    private int GetSonarType(GameObject obj)
    {
        if (obj.name.Contains("Beacon")) return 1;
        if (obj.TryGetComponent(out IA_Fish fish) && Fish.GetFishFromName(fish.name).discovered) return 0;
        if (obj.name.Contains("Abysses")) return 3;
        return 2;
    }

    private void ChangeSprite(Light2D light, Sprite sprite)
    {
        var lightCookieSprite =  typeof( Light2D ).GetField( "m_LightCookieSprite", BindingFlags.NonPublic | BindingFlags.Instance );
        lightCookieSprite.SetValue(light, sprite);
    }

    public IEnumerator FollowTarget(GameObject newLight, GameObject target)
    {
        while (newLight)
        {
            if (!newLight) break;
            newLight.transform.right = target.transform.position - transform.position;
            newLight.transform.GetChild(0).rotation = Quaternion.identity;
            yield return null;
        }
    }

    /*
     * Fonction qui permet d'activer le cercle du sonnar
     */
    private IEnumerator ActivateCircle()
    {
        if(!spotter.gameObject.activeSelf)
        {
            spotter.gameObject.SetActive(true);
            spotter.localScale = Vector3.zero;
            float t = 0;
            while (t < 0.75f)
            {
                float coordinate = t * 2f;
                spotter.localScale = new Vector3(coordinate, coordinate, 1);
                t += Time.deltaTime;
                scanningTime = 3f;
                yield return null;
            }
        }
    }

    /*
     * Fonction qui permet de désactiver le cercle du sonnar
     */
    private void DeactivateSpotter()
    {
        spotter.gameObject.SetActive(false);
        foreach (GameObject lightObject in lights)
        {
            Destroy(lightObject);
        }
        lights.Clear();
    }
}

public enum SonarTypes
{
    Beacon,
    Fish,
    UnknownFish
}