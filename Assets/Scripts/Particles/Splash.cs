using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Splash : MonoBehaviour
{
    public GameObject splash;
    public float spawnRate = 0.2f;
    public float nextSpawn = 1.1f;
    public float speed;
    bool enable = true;
    //private GameManager gameManager;
    //private Vector3 spawnPosition;

    private void Awake()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    void Start()
    {
        //spawnPosition = gameManager.particlePosition[0];
        //gameManager.particlePosition.RemoveAt(0);
    }

    void Update()
    {
        StartCoroutine(KillSystem());
        if (Time.time > nextSpawn && enable)
        {
            nextSpawn = Time.time + (spawnRate);
            Vector2 newPos = /*spawnPosition*/ transform.position + new Vector3(UnityEngine.Random.Range(-100, 100), 0, 0);
            GameObject myObject = Instantiate(splash, newPos, Quaternion.identity);
            myObject.transform.SetParent(transform);
        }
    }

    IEnumerator KillSystem()
    {
        transform.position = transform.position + new Vector3(0, 6, 0);
        yield return new WaitForSecondsRealtime(0.5f);
        
        transform.position = transform.position + new Vector3(0, -12, 0);
        //Debug.Log("HIRSCHRAGOUT!");
        yield return new WaitForSecondsRealtime(1f);
        enable = false;
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }
}
