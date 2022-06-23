using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Beacon : MonoBehaviour
{
    public GameObject nextBeacon;
    public Light2D light;
    public LineRenderer line;
    public Didacticiel didacticiel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (didacticiel) didacticiel.beaconActivated = true;
        if(other.gameObject.CompareTag("Player"))
        {
            light.color = Color.yellow;
            gameObject.tag = "Untagged";

            if (!nextBeacon) return;
            line.gameObject.SetActive(true);
            line.SetPosition(0, transform.GetChild(0).position);
            line.SetPosition(1, nextBeacon.transform.GetChild(0).position);
            nextBeacon.tag = "Interest";
        }
    }
}
