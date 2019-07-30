﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // 1

//Code nur für Drag and Drop Funktion = 1
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler // 1
{
    public Transform parentToReturnTo = null;// sets the parent (Hand)
    public bool draggable = true;
    public bool setsDraggableFalse = false;
    public Vector3 dragOffset;
    

    

   public void OnBeginDrag(PointerEventData eventData) // 1
    {
        
        
        if (draggable)
        {
            
            parentToReturnTo = this.transform.parent;                       // Wenn die angewählte Karte aus der Hand geschoben wird,
            this.transform.SetParent(this.transform.parent.parent);         // Ordnen sich die übrigen neu an.

            // Die Raycasts werden zum Zeiger durch die Karte (CanvasGroup) nicht mehr geblockt. 
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            dragOffset = this.transform.position - new Vector3(eventData.position.x,eventData.position.y,0);
            transform.rotation = Quaternion.identity;
        }
                  
    }
    public void OnDrag(PointerEventData eventData) // 1
    {
        if (draggable)
        {
            //Debug.Log("OnDrag"); // 1
            this.transform.position = new Vector3(eventData.position.x, eventData.position.y, 0) + dragOffset; // 1
        }
        }
    public void OnEndDrag(PointerEventData eventData) // 1
    {
        if (draggable)
        {
            if (setsDraggableFalse)
            {
                setsDraggableFalse = false;
                draggable = false;
            }
            //Debug.Log("OnEndDrag"); // 1
            this.transform.position = new Vector3(eventData.position.x, eventData.position.y, 0) + dragOffset; ;
            this.transform.SetParent(parentToReturnTo);                     // Die Karte wird beim loslassen zurück in die Hand eingeordnet
            GetComponent<CanvasGroup>().blocksRaycasts = true;              // Raycasts werden wieder durch Karte geblockt.
            
        }
    }

    

}       
