using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // 1

//Code nur für Drag and Drop Funktion = 1
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler // 1
{
    public Transform parentToReturnTo = null;                                  // sets the parent (Hand)

   public void OnBeginDrag(PointerEventData eventData) // 1
    {
        Debug.Log("OnBeginDrag"); // 1

        parentToReturnTo = this.transform.parent;                       // Wenn die angewählte Karte aus der Hand geschoben wird,
        this.transform.SetParent(this.transform.parent.parent);         // Ordnen sich die übrigen neu an.

        GetComponent<CanvasGroup>().blocksRaycasts = false;             // Die Raycasts werden zum Zeiger durch die Karte (CanvasGroup) nicht mehr geblockt. 
    }
    public void OnDrag(PointerEventData eventData) // 1
    {
        //Debug.Log("OnDrag"); // 1
        this.transform.position = eventData.position; // 1
    }
    public void OnEndDrag(PointerEventData eventData) // 1
    {
        Debug.Log("OnEndDrag"); // 1
        this.transform.SetParent(parentToReturnTo);                     // Die Karte wird beim loslassen zurück in die Hand eingeordnet
        GetComponent<CanvasGroup>().blocksRaycasts = true;              // Raycasts werden wieder durch Karte geblockt.
    }
}       //22:40
