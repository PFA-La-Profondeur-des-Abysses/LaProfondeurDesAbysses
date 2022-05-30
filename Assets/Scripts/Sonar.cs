using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Sonar : MonoBehaviour
{
    public List<Transform> interestPoints;

    public Transform scan;
    public Transform spotter;
    public Transform light;

    private List<GameObject> lights = new List<GameObject>();
    private float scanningTime;

    /*
     * A chaque frame:
     * vérifie si le joueur veut scanner, puis si un scan est déjà en cours
     * désactive le cercle du sonnar quand celui-ci arrive à expiration
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
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
    }

    /*
     * Lance un scan
     * Le scan grandit au fur et à mesure du temps, et l'objet du scan détecte les objets d'intérêt lui même
     * (donc pas dans ce code)
     */
    private IEnumerator Scan()
    {
        scan.gameObject.SetActive(true);    
        scan.localScale = new Vector3(1, 1, 1);
        scan.position = transform.position;
        scan.parent = transform.root.parent;
        float t = 0;
        while (t < 10)
        {
            float coordinate = t * 20;
            scan.localScale = new Vector3(coordinate, coordinate, 1);
            t += Time.deltaTime;
            scanningTime = 1.5f;
            yield return null;
        }
        StartCoroutine(ActivateSpotter());
        scan.gameObject.SetActive(false);
    }
    
    /*
     * Lorsque l'objet du scan détecte un objet, il lance cette fonction
     * La fonction active le cercle du sonnar, puis affiche l'objet détecté par une lumière
     */
    public IEnumerator DetectObject(Transform objectPosition)
    {
        StartCoroutine(ActivateSpotter());
        GameObject newLight = Instantiate(light.gameObject, spotter);
        newLight.SetActive(true);
        newLight.transform.right = objectPosition.position - transform.position;
        if(objectPosition.name.Contains("Beacon")) 
            newLight.transform.GetChild(0).GetComponent<Light2D>().color = Color.yellow;
        lights.Add(newLight);
        float t = 0;
        while (t < 1f)
        {
            float coordinate = t * 4f;
            newLight.transform.GetChild(0).localPosition = new Vector3(coordinate, 0, 0);
            t += Time.deltaTime;
            scanningTime = 1.5f;
            yield return null;
        }
        light.GetChild(0).localPosition = new Vector3(4, 0, 0);
    }

    /*
     * Fonction qui permet d'activer le cercle du sonnar
     */
    private IEnumerator ActivateSpotter()
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
                scanningTime = 1.5f;
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
