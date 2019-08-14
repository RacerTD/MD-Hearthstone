using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HoverPreview : MonoBehaviour
{
    public bool hovered;
    //1 public Vector3 startRotation;
    //2 Vector3 initialRot = new Vector3();
    //3 public Quaternion originalPos;
    //public bool handRotation;

    public CardsSideBySide CardsSideBySide;
   public Vector3 targetSize;


    public void Start()
    {
        hovered = false;
        //1 startRotation = transform.eulerAngles;
        //2 initialRot = transform.eulerAngles;
        //3 originalPos = transform.rotation;
        //handRotation = true;

        targetSize = transform.eulerAngles;
        //if (handRotation)
        //{
        //    CardsSideBySide.UpdateCardPositions();
        //}


    }
    public void Update()
    {
        //transform.SetPositionAndRotation(MyPosition, Quaternion.Euler(MyRotation));
        // set Card size start and sets size if hovered
        Vector3 targetSize=new Vector3(1f, 1f, 1f);
        if(hovered) targetSize = new Vector3(1.6f, 1.6f, 1.6f);

        // float number regulates the velocity of the preview
        transform.localScale = Vector3.Lerp(transform.localScale, targetSize, 0.12f);
       
    }

    public void OnMouseEnter()
    {
        //Debug.Log("enter");
        // enlarges the Card

        hovered = true;


        //if(hovered) transform.rotation = Quaternion.identity;
        

        //transform.rotation = Quaternion.identity;
    }
    public void OnMouseExit()
    {
        //Debug.Log("Exit GameObject");
      
        hovered = false;
        //1 startRotation = transform.eulerAngles;
        //2 initialRot = transform.eulerAngles;
        //3 transform.rotation = originalPos;
        targetSize = transform.eulerAngles;

    }
}
