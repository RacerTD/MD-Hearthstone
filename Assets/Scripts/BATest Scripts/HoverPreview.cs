using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HoverPreview : MonoBehaviour
{
    public bool hovered;
    //1 public Vector3 startRotation;
    //2 Vector3 initialRot = new Vector3();
    public Quaternion originalPos;
    public void Start()
    {
        hovered = false;
        //1 startRotation = transform.eulerAngles;
        //2 initialRot = transform.eulerAngles;
        originalPos = transform.rotation;
    }
    public void Update()
    {
        Vector3 targetSize=new Vector3(1f, 1f, 1f);
        if(hovered) targetSize = new Vector3(1.6f, 1.6f, 1.6f);

        transform.localScale = Vector3.Lerp(transform.localScale, targetSize, 0.12f);  
    }

    public void OnMouseEnter()
    {
        //Debug.Log("enter");
        // enlarges the Card
        hovered = true;
        transform.rotation = Quaternion.identity;
    }
    public void OnMouseExit()
    {
        //Debug.Log("Exit GameObject");
      
        hovered = false;
        //1 startRotation = transform.eulerAngles;
        //2 initialRot = transform.eulerAngles;
        transform.rotation = originalPos;
    }
}
