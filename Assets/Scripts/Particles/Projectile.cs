using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    //public float spawnRate = 0.2f;
    public float nextSpawn = 1.1f;
    public float speed;
    bool enable = true;
    public Vector3 offset; 
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(KillSystem());
        if (Time.time > nextSpawn && enable)
        {
            Vector2 newPos = transform.position + offset;
            GameObject myObject = Instantiate(projectile, newPos, Quaternion.identity);
            myObject.transform.SetParent(transform);
        }
    }

    IEnumerator KillSystem()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        enable = false;
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }
}
