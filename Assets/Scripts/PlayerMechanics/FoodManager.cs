using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    #region Meat

    private GameObject meat;
    [SerializeField] private List<Sprite> meatSprites;

    #endregion
    #region Leaf

    private GameObject leaf;
    [SerializeField] private List<Sprite> leafSprites;

    #endregion
    private GameObject food;
    private bool canThrow = true;
    private List<GameObject> fodders;

    private void Start()
    {
        leaf = transform.GetChild(0).gameObject;
        meat = transform.GetChild(1).gameObject;
        food = meat;
    }
    
    void Update()
    {
        if (Time.timeScale == 0) return; //EXIT si le rapport est utilisé
        if(Input.GetKeyDown(KeyCode.G) && canThrow)
        {
            food = meat;
            StartCoroutine(ThrowFood());
        }
        if(Input.GetKeyDown(KeyCode.F) && canThrow)
        {
            food = leaf;
            StartCoroutine(ThrowFood());
        }
    }

    private IEnumerator ThrowFood()
    {
        canThrow = false;
        var food = this.food;
        var foodParent = GameObject.Find("Food");
        if (!foodParent) foodParent = Instantiate(new GameObject("Food"));

        for (var i = 0; i < Random.Range(5, 10); i++)
        {
            var newFood = Instantiate(food, transform.position, Quaternion.identity, foodParent.transform);
            newFood.SetActive(true);
            newFood.GetComponent<SpriteRenderer>().sprite =
                food == leaf ? leafSprites[Random.Range(0, 3)] : meatSprites[Random.Range(0, 3)];
            
            newFood.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.5f, 0.5f), -1), ForceMode2D.Impulse);
            StartCoroutine(LowerVelocity(newFood));
            Destroy(newFood, Random.Range(10f, 15f));

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        canThrow = true;
    }

    private IEnumerator LowerVelocity(GameObject foodObject)
    {
        if (!foodObject) yield break;
        var rigidbody = foodObject.GetComponent<Rigidbody2D>();
        if (rigidbody.velocity == Vector2.zero) yield break;

        rigidbody.velocity = Vector2.MoveTowards(rigidbody.velocity, Vector2.zero, Time.deltaTime);

        yield return null;

        StartCoroutine(LowerVelocity(foodObject));
    }

    private void ChangeType()
    {
        food = food == leaf ? meat : leaf;
    }
}
