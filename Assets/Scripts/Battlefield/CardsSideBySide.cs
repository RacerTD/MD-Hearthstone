using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsSideBySide : MonoBehaviour
{
    public float cardOffSetVertical;
    public float cardOffSetHorizontal;
    public float cardOffSetDepth;
    public float rotationOffSet;
    public float scale;
    Transform myChild;
    int childCount;
    float startingAngle;
    float startingPosition;
    void Update()
    {
        if (childCount != transform.childCount)
        {
            UpdateCardPositions();
            //Debug.Log("Baum");
        }
        childCount = transform.childCount;
        UpdateCardPositions();
    }

    
    void UpdateCardPositions()
    {
        startingAngle = -(rotationOffSet * (transform.childCount - 1) / 2);
        startingPosition = - ((transform.childCount - 1) * cardOffSetHorizontal) / 2;

        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            myChild.transform.localPosition = new Vector3((i * cardOffSetHorizontal + startingPosition), ( cardOffSetVertical), (i * cardOffSetDepth));
            myChild.transform.eulerAngles = new Vector3(0, 0, startingAngle + rotationOffSet * i);
            myChild.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}