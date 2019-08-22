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
    private GameManager gameManager;
    private Vector3 spawnPosition;

    public AudioClip damageSFX;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = gameManager.particlePosition[0];
        gameManager.particlePosition.RemoveAt(0);
        //AudioManager.Instance.PlaySFX(damageSFX);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(KillSystem());
        if (Time.time > nextSpawn && enable)
        {
            nextSpawn = Time.time + (spawnRate);
            Vector2 newPos = spawnPosition + new Vector3(UnityEngine.Random.Range(-50, 50), UnityEngine.Random.Range(-50, 50), 0);
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
        Destroy(transform.parent.gameObject);
    }
}
