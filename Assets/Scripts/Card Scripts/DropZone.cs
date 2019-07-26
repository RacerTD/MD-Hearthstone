using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)      //
    {
        //Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.LogWarning(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        Draggable drag = eventData.pointerDrag.GetComponent<Draggable>();       // parentToReturnTo wird überschrieben,
        if (drag != null)                                                       // sodass die Karten 
        {
            drag.parentToReturnTo = this.transform;                             // beim Loslassen in die Dropzone gelgt werden.
        }
    }
}
