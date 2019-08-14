using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{


    [SerializeField]
    private GameObject ArrowPrefab;
    [SerializeField]
    private GameObject ArrowPointPrefab;
    private UILineRenderer UILineRenderer;
    List<GameObject> array = new List<GameObject>();
    bool active = true;
    GameObject newLineGen;
    Vector3 secondPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

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
            Vector3 newPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            
            newPos.z = 0;
            CreatePointMarker(newPos);

        }
        if(Input.GetMouseButton(0))
        {

        }
        if (active)
        {
            if (array.Count == 1)
            {
                
                newLineGen.GetComponent<UILineRenderer>();

                newLineGen.transform.position = Input.mousePosition;
            }

            if (array.Count >= 2)
            {
                active = false;

            }
        }
    }
    public void LineRenderer()
    {
        newLineGen = Instantiate(ArrowPrefab);

        UILineRenderer lRend = newLineGen.GetComponent<UILineRenderer>();

        lRend.transform.SetParent(transform);
    }

    public void CreatePointMarker(Vector3 pointPosition)
    {
        
        GameObject arrowinstance = Instantiate(ArrowPointPrefab, pointPosition, Quaternion.identity);
        array.Add(arrowinstance);
        
        arrowinstance.transform.SetParent(transform);
        GenerateNewLine();
    }

    private void ClearAllPoints()
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("PointMarker");

        foreach (GameObject p in allPoints)
        {
            Destroy(p);
        }
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