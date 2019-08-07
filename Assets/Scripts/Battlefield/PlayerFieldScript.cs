using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFieldScript : MonoBehaviour
{
    int childCount = 0;
    Transform myChild;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (childCount != transform.childCount)
        {
            UpdateCardParts();
        }
        childCount = transform.childCount;
    }

    void UpdateCardParts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            myChild.GetComponent<OneCardManager>().NowOnField();
            myChild.GetComponent<Draggable>().enabled = false;
        }
    }
}
