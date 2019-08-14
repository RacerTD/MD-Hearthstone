using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Heal : MonoBehaviour
{
    public GameObject healParticle;
    public float spawnRate = 0.2f;
    public float nextSpawn = 1.1f;
    public float speed;
    bool enable = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(KillSystem());
        if (Time.time > nextSpawn && enable)
        {
            nextSpawn = Time.time + (spawnRate + 0.2f);
            Vector2 newPos = transform.position + new Vector3(UnityEngine.Random.Range(-50, 50), 0, 0);
            GameObject myObject = Instantiate(healParticle, newPos, Quaternion.identity);
            myObject.transform.SetParent(transform);
        }
    }

    IEnumerator KillSystem()
    {
        yield return new WaitForSecondsRealtime(2f);
        enable = false;
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
    }
}
