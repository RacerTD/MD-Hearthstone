using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public float startingAngle;
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
            childCount = transform.childCount;
            UpdateCardPositions();
            //Debug.Log("Baum");
        }
        //UpdateCardPositions();
    }

    
   public void UpdateCardPositions()
    {
        startingAngle = -(rotationOffSet * (transform.childCount - 1) / 2);
        startingPosition = - ((transform.childCount - 1) * cardOffSetHorizontal) / 2;

        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            Vector3 targetPosition;
            if (randomVertical && randomHorizontal)
            {
                targetPosition=new Vector3((i * cardOffSetHorizontal + startingPosition + horizontalOffset[i]), (verticalOffset_ + verticalOffset[i]), (i * cardOffSetDepth));
            }
            else if (randomVertical)
            {
                targetPosition=new Vector3((i * cardOffSetHorizontal + startingPosition), (verticalOffset_ + verticalOffset[i]), (i * cardOffSetDepth));
            }
            else
            {
                targetPosition=new Vector3((i * cardOffSetHorizontal + startingPosition), (verticalOffset_ - (Mathf.Abs(startingAngle + rotationOffSet * i) * 2)), (i * cardOffSetDepth));
            }

            if (myChild.GetComponent<OneCardManager>())
                myChild.GetComponent<OneCardManager>().targetPosition = targetPosition;
            myChild.transform.DOLocalMove(targetPosition, 1f).SetEase(Ease.OutQuart);

            Vector3 targetRotation;

            if (randomAngle)
            {
                targetRotation=new Vector3(0, 0, startingAngle + rotationOffSet * i + angleOffset[i]);
                myChild.transform.DOScale(new Vector3(scale, scale, scale),1f).SetEase(Ease.OutQuart);
            }
            else
            {
                targetRotation=new Vector3(0, 0, startingAngle + rotationOffSet * i);
                myChild.transform.DOScale(new Vector3(scale, scale, scale),1f).SetEase(Ease.OutQuart);
            }

            if (myChild.GetComponent<OneCardManager>())
                myChild.GetComponent<OneCardManager>().targetRotation = targetRotation;
            myChild.transform.DOLocalRotate(targetRotation, 1f).SetEase(Ease.OutQuart);
        }

    }
}