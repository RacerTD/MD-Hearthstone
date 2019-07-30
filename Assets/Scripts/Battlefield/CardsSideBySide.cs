using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsSideBySide : MonoBehaviour
{
    public List<float> verticalOffset = new List<float>();
    public bool randomVertical;
    public float verticalOffset_;

    public float cardOffSetHorizontal;
    public float cardOffSetDepth;
    public float rotationOffSet;
    public float scale;
    Transform myChild;
    int childCount;
    float startingAngle;
    float startingPosition;
    private void Start()
    {
        for (int k = 0; k < 20; k++)
        {
            verticalOffset.Add(Random.Range(-25, 25));
        }
    }
    void Update()
    {
        if (childCount != transform.childCount)
        {
            UpdateCardPositions();
            //Debug.Log("Baum");
        }
        childCount = transform.childCount;
        //UpdateCardPositions();
    }

    
    void UpdateCardPositions()
    {
        startingAngle = -(rotationOffSet * (transform.childCount - 1) / 2);
        startingPosition = - ((transform.childCount - 1) * cardOffSetHorizontal) / 2;

        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            if (randomVertical)
            {
                myChild.transform.localPosition = new Vector3((i * cardOffSetHorizontal + startingPosition), (verticalOffset_ + verticalOffset[i]), (i * cardOffSetDepth));
            }
            else
            {
                myChild.transform.localPosition = new Vector3((i * cardOffSetHorizontal + startingPosition), verticalOffset_, (i * cardOffSetDepth));
            }
            
            myChild.transform.eulerAngles = new Vector3(0, 0, startingAngle + rotationOffSet * i);
            myChild.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}