using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private LineRenderer lineRenderer;
    Vector2 startPos;
    Vector2 endPos;
    Camera camera;
    Vector3 camOffset = new Vector3(0, 0, 5);
    [SerializeField] AnimationCurve ac;
    void Start()
    {
        camera = Camera.main;
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (lineRenderer  == null)
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
            }
            lineRenderer.positionCount = 2;
            startPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.useWorldSpace = true;
            if (Input.GetMouseButton(0))
            {
                endPos = camera.ScreenToWorldPoint(Input.mousePosition);
                lineRenderer.SetPosition(1, endPos);
            }
        }
        
    }
    
}
