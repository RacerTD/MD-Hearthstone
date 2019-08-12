using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [Attribut]: Code will also be called in Editor Mode
[ExecuteInEditMode]
public class CardRotation : MonoBehaviour
{
    public RectTransform CardFront;

    public RectTransform CardBack;

    // an empty Game Object that is placed a bit above the face of the card, in the center of the card
    public Transform targetFacePoint;

    public Collider collision;

    // if this is true, player see the card Back
    private bool showingBack = false;

    // called once per frame
    void Update()
    {
        // Raycast from Camera to a target point on the face of the card
        // If it passes through the card's collider, we should show the back of the card

        RaycastHit[] hits;
        hits = Physics.RaycastAll(origin: Camera.main.transform.position,
            // direction: target - Camera. / .normalized means magnitude is equal to 1 
            direction: (-Camera.main.transform.position + targetFacePoint.position).normalized,
            maxDistance: (-Camera.main.transform.position + targetFacePoint.position).magnitude) ;


        bool passedThroughColliderOnCard = false;
       
        // sets bool passedThruoghColliderOnCard to true if collider gets hit
        foreach (RaycastHit hitsHit in hits)
        {
            if (hitsHit.collider == collision)
                passedThroughColliderOnCard = true;
        }

        if (passedThroughColliderOnCard != showingBack)
        {
            showingBack = passedThroughColliderOnCard;
            if (showingBack)
            {
                CardFront.gameObject.SetActive(false);
                CardBack.gameObject.SetActive(true);
            }
            else
            {
                CardFront.gameObject.SetActive(true);
                CardBack.gameObject.SetActive(false);
            }

        }





    }
}
