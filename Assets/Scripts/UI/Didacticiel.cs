using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Didacticiel : MonoBehaviour
{
    public List<Animator> spawners;
    public List<Animator> controls;
    private int step;
    public bool beaconActivated;
    public bool rapportRempli;

    private List<Collider2D> results = new();
    private ContactFilter2D contactFilter2D;

    // Start is called before the first frame update
    void Start()
    {
        Activate();
        contactFilter2D.SetLayerMask(LayerMask.GetMask("Player"));
        contactFilter2D.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case 0:
                if (!(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Q) ||
                      Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
                    return;

                StartCoroutine(Deactivate(0, 1));
                ActivateControl();
                step++;
                Activate();
                beaconActivated = false;
                break;
            case 1:
                if(!beaconActivated) return;
                
                Deactivate();
                step++;
                Activate();
                break;
            case 2:
                if (!Input.GetKeyDown(KeyCode.Space)) return;
                
                StartCoroutine(Deactivate(2, 1));
                ActivateControl(1);
                step++;
                Activate();
                break;
            case 3:
                if(!(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.A))) return;
                if (!IsPlayerNear()) return;
                
                StartCoroutine(Deactivate(3, 2));
                ActivateControl(2);
                ActivateControl(3);
                step++;
                Activate();
                break;
            case 4:
                if(!Input.GetKeyDown(KeyCode.Tab)) return;
                
                Deactivate();
                ActivateControl(4);
                step++;
                Activate();
                rapportRempli = false;
                break;
            case 5:
                if(!rapportRempli) return;
                
                Deactivate();
                step++;
                Activate();
                break;
            case 6:
                if(!RapportManager.rapportManager.GetCurrentPageImage().sprite) return;
                
                Deactivate();
                step++;
                Activate();
                break;
            case 7:
                if(!Input.GetKeyDown(KeyCode.LeftShift)) return;
                
                Deactivate();
                step++;
                Activate();
                break;
            case 8:
                if(!Input.GetKeyDown(KeyCode.F)) return;
                
                Deactivate();
                ActivateControl(5);
                step++;
                break;
            default:
                Destroy(gameObject, 1);
                break;
        }
    }

    private bool IsPlayerNear()
    {
        return spawners[step].GetComponent<BoxCollider2D>().OverlapCollider(contactFilter2D, results) > 0;
    }

    #region Spawners

    private void Activate()
    {
        spawners[step].SetTrigger("On");
    }

    private void Deactivate()
    {
        spawners[step].SetTrigger("Off");
    }

    private void Activate(int step)
    {
        spawners[step].SetTrigger("On");
    }

    private void Deactivate(int step)
    {
        spawners[step].SetTrigger("Off");
    }

    private IEnumerator Activate(int step, float delay)
    {
        yield return new WaitForSeconds(delay);
        spawners[step].SetTrigger("On");
    }

    private IEnumerator Deactivate(int step, float delay)
    {
        yield return new WaitForSeconds(delay);
        spawners[step].SetTrigger("Off");
    }

    #endregion

    #region Controls

    private void ActivateControl()
    {
        controls[step].SetTrigger("On");
    }

    private void DeactivateControl()
    {
        controls[step].SetTrigger("Off");
    }

    private void ActivateControl(int step)
    {
        controls[step].SetTrigger("On");
    }

    private void DeactivateControl(int step)
    {
        controls[step].SetTrigger("Off");
    }

    private IEnumerator ActivateControl(int step, float delay)
    {
        yield return new WaitForSeconds(delay);
        controls[step].SetTrigger("On");
    }

    private IEnumerator DeactivateControl(int step, float delay)
    {
        yield return new WaitForSeconds(delay);
        controls[step].SetTrigger("Off");
    }

    #endregion
    
    


    public void RegimeChange()
    {
        rapportRempli = true;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        foreach (var control in controls)
        {
            if(control.transform.localScale == Vector3.zero) control.SetTrigger("On");
        }
        Destroy(gameObject, 1);
    }
}
