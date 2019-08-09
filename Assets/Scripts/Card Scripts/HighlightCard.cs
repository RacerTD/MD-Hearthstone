using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightCard : MonoBehaviour
{

    public Image[] toHighlight = new Image[2];
    public GameObject gameManager;
    private CardType currendCardInHand;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }


    void Update()
    {
        currendCardInHand = gameManager.GetComponent<GameManager>().cardInHand;

        if (currendCardInHand == CardType.Epuipment && gameObject.GetComponent<OneCardManager>().equipmentCount <= 4)
        {
            Debug.Log("Highlight equipment");
        }
    }
}
