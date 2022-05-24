using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BeaconManager : MonoBehaviour
{
    public bool floor;
    
    [SerializeField] private GameObject beaconOrigin;
    [SerializeField] private Transform beacons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (floor)
            {
                SetBeaconOnFloor();
            }
            else
            {
                SetBeaconInTheAir();
            }
        }
    }

    /*
     * Détecte si le joueur est à une distance suffisante du sol et pose la balise en dessous de celui-ci
     */
    private void SetBeaconOnFloor()
    {
        Debug.Log(LayerMask.LayerToName(LayerMask.NameToLayer("Floor")));
        RaycastHit2D cast = Physics2D.Raycast(transform.position, Vector2.down,
            10, LayerMask.GetMask("Floor"));
        if (cast.collider == null) return;
        Debug.Log("floor");
        Debug.Log(cast.collider.name);
        GameObject beacon = Instantiate(beaconOrigin, beacons);
        beacon.SetActive(true);
        beacon.transform.position = cast.point;
    }   
    
    /*
     * Pose la balise devant le joueur
     */
    private void SetBeaconInTheAir()
    {
        GameObject beacon = Instantiate(beaconOrigin, beacons);
        beacon.SetActive(true);
        beacon.transform.position = beaconOrigin.transform.position;
    }
}
