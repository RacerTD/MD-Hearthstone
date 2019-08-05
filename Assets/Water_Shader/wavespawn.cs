using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wavespawn : MonoBehaviour
{
    public GameObject wave;
    public GameObject Cards;
    public float spawnDelay = 0.2f;
    float nextSpawn = 1.1f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnDelay*0.01f;
            Vector2 newPos = new Vector2(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(180, Screen.height));
            GameObject myObject = Instantiate(wave, newPos, Quaternion.identity);
            myObject.transform.SetParent(transform);
        }
    }
}
