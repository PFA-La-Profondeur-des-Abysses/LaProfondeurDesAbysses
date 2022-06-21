using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Beacon : MonoBehaviour
{
    public GameObject nextBeacon;
    public Light2D light;
    public LineRenderer line;

    void OnTriggerEnter2D(Collider2D other)
    {
        var script = transform.GetComponentInChildren<Spawner_Destroyer>();
        if (script)
        {
            script.ActivateText();
            script.GetComponent<BoxCollider2D>().enabled = true;
        }
        if(other.gameObject.CompareTag("Player"))
        {
            light.color = Color.yellow;
            gameObject.tag = "Untagged";

            if (!nextBeacon) return;
            line.gameObject.SetActive(true);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, nextBeacon.transform.position);
            nextBeacon.tag = "Interest";
        }
    }
}
