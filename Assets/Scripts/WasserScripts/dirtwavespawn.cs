using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dirtwavespawn : MonoBehaviour
{
    public GameObject wave;
    public float spawnRate = 0.2f;
    float nextSpawn = 1.1f;
    float speed;
    public Color dirtyColor;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + (spawnRate + 0.2f);
            Vector2 newPos = new Vector2(UnityEngine.Random.Range(200, Screen.width - 200), UnityEngine.Random.Range(Screen.height * 0.7f, Screen.height * 0.95f));
            GameObject myObject = Instantiate(wave, newPos, Quaternion.identity);
            myObject.GetComponent<Image>().color = dirtyColor;
            myObject.transform.SetParent(transform);
            transform.position += new Vector3(0, newPos.x + 100, Time.deltaTime);
        }
    }
}
