using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HoverPreview : MonoBehaviour
{
    public GameObject toHover;

    [Tooltip("This will be multiplied with our standard scale (1)")]
    public float targetScale = 1.6f;
    public bool hovered
    {
        get { return _hovered; }
        set
        {
            _hovered = value;
            isHovering = value;
        }
    }
    private bool _hovered;
    //1 public Vector3 startRotation;
    //2 Vector3 initialRot = new Vector3();
    //3 public Quaternion originalPos;
    //public bool handRotation;

    public static bool isHovering;


    public void Start()
    {

        toHover = toHover ?? gameObject;
        hovered = false;
        //1 startRotation = transform.eulerAngles;
        //2 initialRot = transform.eulerAngles;
        //3 originalPos = transform.rotation;
        //handRotation = true;

        
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
        if(hovered) targetSize = new Vector3(targetScale, targetScale, targetScale);

        // float number regulates the velocity of the preview
        //transform.localPosition = Vector3.Lerp(transform.localPosition, transform.position. 0);
        transform.localScale = Vector3.Lerp(transform.localScale, targetSize, 0.12f);
       
    }

    public void OnMouseEnter()
    {
        Debug.Log("enter");
        // enlarges the Card
        if (isHovering) return;

        hovered = true;


        if (hovered)
        {
            transform.rotation = Quaternion.identity;
            //transform.eulerAngles = new Vector3(0, 0, 0);
            //transform.rotation = Quaternion.identity;
        }

        //transform.rotation = Quaternion.identity;
    }
    public void OnMouseExit()
    {
        //Debug.Log("Exit GameObject");

        GameManager.Main.cardsSideBySide.UpdateCardPositions();

        hovered = false;
        
        //1 startRotation = transform.eulerAngles;
        //2 initialRot = transform.eulerAngles;
        //3 transform.rotation = originalPos;
        

    }
}
