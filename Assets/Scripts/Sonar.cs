using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                StartCoroutine(Scan());
                DeactivateSpotter();
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
        float t = 0;
        while (t < 10)
        {
            float coordinate = t * 20;
            scan.localScale = new Vector3(coordinate, coordinate, 1);
            t += Time.deltaTime;
            scanningTime = 1.5f;
            yield return null;
        }
        ActivateSpotter();
        scan.gameObject.SetActive(false);
    }
    
    /*
     * Lorsque l'objet du scan détecte un objet, il lance cette fonction
     * La fonction active le cercle du sonnar, puis affiche l'objet détecté par une lumière
     */
    public void DetectObject(Vector3 objectPosition)
    {
        ActivateSpotter();
        GameObject newLight = Instantiate(light.gameObject, spotter);
        newLight.SetActive(true);
        newLight.transform.right = objectPosition - transform.position;
        lights.Add(newLight);
    }

    /*
     * Fonction qui permet d'activer le cercle du sonnar
     */
    private void ActivateSpotter()
    {
        spotter.gameObject.SetActive(true);
        scanningTime = 1.5f;
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
