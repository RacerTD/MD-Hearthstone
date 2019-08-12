using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{


    [SerializeField]
    private GameObject ArrowPrefab;
    [SerializeField]
    private GameObject ArrowPointPrefab;


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            CreatePointMarker(newPos);
        }

        

        if (Input.GetKeyDown("e"))
        {
            GenerateNewLine();
        }
    }

    private void CreatePointMarker(Vector3 pointPosition)
    {
        GameObject arrowinstance = Instantiate(ArrowPointPrefab, pointPosition, Quaternion.identity);
        arrowinstance.transform.SetParent(transform);
    }

    private void ClearAllPoints()
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("PointMarker");

        foreach (GameObject p in allPoints)
        {
            Destroy(p);
        }
    }

    private void GenerateNewLine()
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("PointMarker");
        Vector3[] allPointPositions = new Vector3[allPoints.Length];

        if (allPoints.Length >= 2)
        {
            for (int i = 0; i < allPoints.Length; i++)
            {
                allPointPositions[i] = allPoints[i].transform.position;
            }

            SpawnLineGenerator(allPointPositions);
        }
    }

    private void SpawnLineGenerator(Vector3[] linePoints)
    {
        Debug.Log("Hewwo");
        GameObject newLineGen = Instantiate(ArrowPrefab);

        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();

        lRend.positionCount = linePoints.Length;
        lRend.SetPositions(linePoints);
        lRend.transform.SetParent(transform);

        ClearAllPoints();
        Destroy(newLineGen, 5);
    }
}