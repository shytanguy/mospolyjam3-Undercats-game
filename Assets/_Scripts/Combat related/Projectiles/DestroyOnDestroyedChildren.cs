using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDestroyedChildren : MonoBehaviour
{
    private SpawnableObject[] objects;

    private void Start()
    {
        objects = new SpawnableObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(i);
            objects[i] = transform.GetChild(i).GetComponent<SpawnableObject>();
            objects[i].OnDestroyed += CheckDestroyed;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
         
            objects[i].OnDestroyed -= CheckDestroyed;
        }
    }
    private void CheckDestroyed()
    {
        if (transform.childCount <= 1)
        {
            Destroy(gameObject);
        }
    }
}
