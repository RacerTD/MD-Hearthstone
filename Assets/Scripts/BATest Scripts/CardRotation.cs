using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [Attribut]: Code will also be called in Editor Mode
[ExecuteInEditMode]
public class CardRotation : MonoBehaviour
{
    public RectTransform CardFront;

    public RectTransform CardBack;

    //bool showingFront = false;

    private void Start()
    {
        //Hardset der Kartenrotation um 180 Grad
        transform.eulerAngles = new Vector3(0, 180, 0);
        //showingFront = false;
    }

    // called once per frame
    void Update()
    {

        CalculateRotation();

    }

    private void CalculateRotation()
    {
        float x = transform.rotation.eulerAngles.y;

        // Berechnung der Rotation (Frontside/Backside) mit Hilfe einer Sinus-Funktion.
        // Im positiven Bereich (+1) wird CardFront gezeigt; Im negativen (-1) wird CardBack gezeigt.
        float FrontBackSolution = Mathf.Sin(x / (180 / Mathf.PI) + Mathf.PI / 2);

        float signFB = Mathf.Sign(FrontBackSolution);

        if (signFB == 1) //&& !showingFront)
        {
            CardFront.gameObject.SetActive(true);
            CardBack.gameObject.SetActive(false);
            //showingFront = true;
        }
        else if (signFB == -1) //&& showingFront)
        {
            CardFront.gameObject.SetActive(false);
            CardBack.gameObject.SetActive(true);
            //showingFront = false;
        }
    }



   
}
