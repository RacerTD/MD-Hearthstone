using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropZoneManager : MonoBehaviour
{
    [Header("Drop Zones")]
    public Image playerField;
    public Image enemyField;
    public Image hand;
    public Image mana;
    public Image manPower;


    public GameManager gameManager;
    public CardType currentlyDragging;

    // Start is called before the first frame update
    void Start()
    {
        currentlyDragging = CardType.Nothing;
        DeactivateAllDropZones();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyDragging != gameManager.cardInHand)
        {
            currentlyDragging = gameManager.cardInHand;

            switch (currentlyDragging)
            {
                case CardType.Human:
                    playerField.enabled = true;
                    mana.enabled = true;
                    manPower.enabled = true;
                    break;

                case CardType.Epuipment:
                    ActivateHumanDropZones();
                    break;

                case CardType.AOEDMGSpell:
                    enemyField.enabled = true;
                    break;

                case CardType.AOEHealSpell:
                    playerField.enabled = true;
                    break;

                case CardType.Enemy:
                case CardType.Egg:
                case CardType.Nothing:
                    DeactivateAllDropZones();
                    break;

            }
        }
    }

    private void DeactivateAllDropZones()
    {
        playerField.enabled = false;
        enemyField.enabled = false;
        hand.enabled = false;
        mana.enabled = false;
        manPower.enabled = false;
    }

    private void ActivateHumanDropZones()
    {
        //Code
    }

}
