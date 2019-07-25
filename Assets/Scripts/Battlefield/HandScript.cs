using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    int childCounter = 0;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (transform.childCount > 0)
        {
            do
            {
                this.gameObject.GetComponentInChildren<OneCardManager>().HandPosition(childCounter);
                childCounter++;
            } while (transform.childCount > childCounter);
            childCounter = 0;
        }
        
    }

}
