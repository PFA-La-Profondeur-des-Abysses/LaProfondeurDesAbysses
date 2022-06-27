using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStreaming : MonoBehaviour
{
    public int level;
    public List<Transform> objects;

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var o in objects)
        {
            o.GetChild(level).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        foreach (var o in objects)
        {
            o.GetChild(level).gameObject.SetActive(false);
        }
    }
}
