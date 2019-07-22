using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool dragging;
    private Vector3 pointerDisplacement;
    private float zDisplacement;
    private DraggingActions da;

    private static Draggable _draggingThis;
    private static Draggable DraggingThis
    {
        get { return _draggingThis; }
    }
    void Awake()
    {
        da = GetComponent<DraggingActions>();
    }

    void OnMeouse()
    {
        
    }
}
