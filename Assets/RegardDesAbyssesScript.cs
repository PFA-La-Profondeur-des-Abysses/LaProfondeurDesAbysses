using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RegardDesAbyssesScript : MonoBehaviour
{
    public Animator animator;
    public bool playAnim;
    public PlayerMovement pm;
    public GameObject player;
    public GameObject CanvasFin;
    public float timerfin = 3f;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindObjectOfType<PlayerMovement>();
        player = pm.gameObject;
        animator = GetComponent<Animator>();
        playAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playAnim)
        {
             player.GetComponent<Rigidbody2D>().velocity /= 1.01f;

             animator.SetBool("OpenEye", true);

            timerfin -= Time.deltaTime;
           
            if(timerfin< 0)
            {
                CanvasFin.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent.GetComponentInParent<PlayerMovement>().canMove = false;

            player.GetComponent<Rigidbody2D>().gravityScale = 0f;
            playAnim = true;


        }
    }

    public void RetourMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
