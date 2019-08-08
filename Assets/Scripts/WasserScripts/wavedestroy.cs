using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavedestroy : MonoBehaviour
    
{
    float deathtime;

    private wavespawn ws;
    void Start()
    {
        ws = GameObject.Find("Waves").GetComponent<wavespawn>();
        deathtime = Time.time + 3.04f;
        
    }

    
    void Update()
    {
        var pos = transform.position;
        pos.x += 0.2f;
        transform.position = pos;
        if ( Time.time > deathtime)
        {
            Destroy(gameObject);
        }
    }
}
