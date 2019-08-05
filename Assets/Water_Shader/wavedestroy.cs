using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavedestroy : MonoBehaviour
    
{
    float deathtime;
    void Start()
    {
        deathtime = Time.time + 1.483f;
    }

    
    void Update()
    {
       if ( Time.time > deathtime)
        {
            Destroy(gameObject);
        }
    }
}
