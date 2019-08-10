using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsSideBySide : MonoBehaviour
{
    public List<float> verticalOffset = new List<float>();
    public List<float> horizontalOffset = new List<float>();
    public List<float> angleOffset = new List<float>();
    public bool randomVertical;
    public bool randomHorizontal;
    public bool randomAngle;
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
            verticalOffset.Add(Random.Range(-40, 40));
            horizontalOffset.Add(Random.Range(-20, 20));
            angleOffset.Add(Random.Range(-10, 10));
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
            if (randomVertical && randomHorizontal)
            {
                myChild.transform.localPosition = new Vector3((i * cardOffSetHorizontal + startingPosition + horizontalOffset[i]), (verticalOffset_ + verticalOffset[i]), (i * cardOffSetDepth));
            }
            else if (randomVertical)
            {
                myChild.transform.localPosition = new Vector3((i * cardOffSetHorizontal + startingPosition), (verticalOffset_ + verticalOffset[i]), (i * cardOffSetDepth));
            }
            else
            {
                myChild.transform.localPosition = new Vector3((i * cardOffSetHorizontal + startingPosition), (verticalOffset_ - (Mathf.Abs(startingAngle + rotationOffSet * i) * 2)), (i * cardOffSetDepth));
            }
            
            if (randomAngle)
            {
                myChild.transform.eulerAngles = new Vector3(0, 0, startingAngle + rotationOffSet * i + angleOffset[i]);
                myChild.transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                myChild.transform.eulerAngles = new Vector3(0, 0, startingAngle + rotationOffSet * i);
                myChild.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}