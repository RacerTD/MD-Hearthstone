using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Arrow : MonoBehaviour
{


    [SerializeField]
    private GameObject ArrowPrefab;
    private Image ArrowPointImage;
    [SerializeField]
    //private GameObject ArrowPointPrefab;
    private UILineRenderer _UILineRenderer;
    List<GameObject> arrayOfLinePoints = new List<GameObject>();
    bool active = true;
    GameObject newLineGen;

    public bool ArrowActive = false;

    public void Start()
    {
        LineRenderer();
    }
    private void Update()
    {
        mouseButtonDown();

    }
    private void mouseButtonDown()
    {
       

            if (Input.GetMouseButtonDown(0))
        {
           
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -20), new Vector3(0, 0, 300), out hit, 10000))
            {
                //Debug.DrawRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -20), new Vector3(0, 0, 300), Color.red);
                Debug.Log("Hit ");
            
                // if hit.GetComponent
                Debug.Log("clicked on object");
                //ADD IN CHECK if can attack 
               SpawnArrowPrefab(hit.transform.position);
            }         
        }

            // right click
        if (Input.GetMouseButtonDown(1))
        {
            if (ArrowActive)
            {
                _UILineRenderer.Points = null;

                //Disable Arrow On right click
            }

            ArrowActive = false;
        }

        // if we already click on a card that can attack, make second point of line renderer follow the mouse
        if (ArrowActive)
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 0;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
            Debug.Log("moving point");

            Vector2[] allPointPositions = new Vector2[2];

            allPointPositions[0] = _UILineRenderer.Points[0];

            allPointPositions[1] = worldPoint;

            _UILineRenderer.Points = allPointPositions;

            RotateAndMoveArrow();
            //Debug.DrawLine(allPointPositions[0], allPointPositions[1], Color.green);

   
        }
    }
    public void LineRenderer()
    {
        /*newLineGen = Instantiate(ArrowPrefab);

        UILineRenderer lRend = newLineGen.GetComponent<UILineRenderer>();

        lRend.transform.SetParent(transform);
        */
    }

    public void RotateAndMoveArrow()
    {
        ArrowPointImage.transform.position = _UILineRenderer.Points[1];

        Vector3 diff = Camera.main.ScreenToWorldPoint(_UILineRenderer.Points[0]) - ArrowPointImage.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        ArrowPointImage.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public void SpawnArrowPrefab(Vector3 pointPosition)
    {
        ArrowActive = true;
        GameObject ArrowGameObject = Instantiate(ArrowPrefab, pointPosition, Quaternion.identity, transform);

        _UILineRenderer = ArrowGameObject.GetComponent<UILineRenderer>();
        ArrowPointImage = GetComponentInChildren<Image>();

        if (_UILineRenderer == null)
        Debug.Log("shit");

        //Vector2[] allPointPositions = new Vector2[2];

        //allPointPositions[0] = pointPosition;
        //allPointPositions[1] = pointPosition;
        //Debug.DrawLine(allPointPositions[0], allPointPositions[1], Color.green);
       _UILineRenderer.Points[0] = new Vector2(pointPosition.x-400, pointPosition.y);
        Debug.Log(pointPosition);
    }

    private void ClearAllPoints()
    {   
        /*
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("PointMarker");

        foreach (GameObject p in allPoints)
        {
            Destroy(p);
        }*/
    }

    public void GenerateNewLine()
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("PointMarker");
        Vector2[] allPointPositions = new Vector2[allPoints.Length];

        
        if (allPoints.Length == 2)
        {

            ClearAllPoints();

        }
        if (allPoints.Length >= 1)
        {
            for (int i = 0; i < allPoints.Length; i++)
            {
                allPointPositions[i] = allPoints[i].transform.position;
                SpawnLineGenerator(allPointPositions);
            }
            
        }
        
    }

    private void SpawnLineGenerator(Vector2[] linePoints)
    {
        newLineGen = Instantiate(ArrowPrefab);

        UILineRenderer lRend = newLineGen.GetComponent<UILineRenderer>();

        int length = lRend.GetComponent<UILineRenderer>().Points.Length;
        length = linePoints.Length;
        lRend.Points = linePoints;
        
        lRend.transform.SetParent(transform);
        

        
        Destroy(newLineGen, 2f);
    }
}