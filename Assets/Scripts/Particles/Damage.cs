using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Damage : MonoBehaviour
{
    public GameObject damageParticle;
    //public float spawnRate = 0.2f;
    public float nextSpawn = 1.1f;
    public float speed;
    bool enable = true;
    private GameManager gameManager;
    private Vector3 spawnPosition;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        spawnPosition = gameManager.particlePosition[0];
        gameManager.particlePosition.RemoveAt(0);
    }

    void Update()
    {
        

        StartCoroutine(KillSystem());
        if (Time.time > nextSpawn && enable)
        {
            //nextSpawn = Time.time + (spawnRate + 0.2f);
            Vector2 newPos = spawnPosition + new Vector3(0,0,0); //transform.position durch spawnPosition erstetzen
            GameObject myObject = Instantiate(damageParticle, newPos, Quaternion.identity);
            myObject.transform.SetParent(transform);
        }
    }

    IEnumerator KillSystem()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        enable = false;
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
}
