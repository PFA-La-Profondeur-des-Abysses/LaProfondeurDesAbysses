using UnityEngine;

public class BeaconManager : MonoBehaviour
{
    public bool floor;
    
    [SerializeField] private GameObject beaconOrigin;
    [SerializeField] private Transform beacons;


    void Update()
    {
        if (Time.timeScale == 0) return;

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
        RaycastHit2D cast = Physics2D.Raycast(transform.position, Vector2.down,
            10, LayerMask.GetMask("Floor"));
        if (cast.collider == null) return;
            //vérifie si le joueur est assez près du sol
        GameObject beacon = Instantiate(beaconOrigin, beacons);
            //Instantie une balise
        beacon.SetActive(true);
        beacon.transform.position = cast.point;
            //set la position de la balise sur le sol le plus proche directement en dessous
    }   
    
    /*
     * Pose la balise devant le joueur
     */
    private void SetBeaconInTheAir()
    {
        GameObject beacon = Instantiate(beaconOrigin, beacons);
            //Instantie une balise
        beacon.SetActive(true);
        beacon.transform.position = beaconOrigin.transform.position;
            //set la position de la balise directement devant le joueur (elle tombe ensuite d'elle même au sol)
        if(beacons.childCount > 10) Destroy(beacons.GetChild(0).gameObject);
    }
}
