using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_poisson : MonoBehaviour
{

    public float Speed;
    public Texture2D[] Test;
    public Sprite[] Poisson;

    private int img = 0;

    private SpriteRenderer ok;

    void Start()
    {
        ok = GetComponent<SpriteRenderer>();
        img = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        img += Mathf.RoundToInt(Time.deltaTime * Speed);
        if(img > Test.Length)
        {
            img = 0;
        }
        else if (img < Test.Length)
        {
            ok.material.SetTexture("_Emissive", Test[img]);
            ok.sprite = Poisson[img];
        }
    }
}
