﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using TMPro;

public class HighlightCard : MonoBehaviour
{

    public List<Image> waves = new List<Image>();
    public List<Image> healAbility = new List<Image>();
    public List<Image> attackAbility = new List<Image>();
    public List<Image> bigImages = new List<Image>();

    public GameManager gameManager;
    public EnemyFieldScript enemyField;
    private bool summoningSickness;
    private CardType currendCardInHand = CardType.Nothing;
    private Highlight currenHighlight = Highlight.Nothing;
    private bool healAbilityUsed = false;
    private bool attackAbilityUsed = false;
    private bool seeMaxLife = false;
    private bool attackUsed = false;
    public TextMeshProUGUI maxLife;
    public TextMeshProUGUI currentLife;

    private Vector3 originalPosition = new Vector3(0, 0, 0);
    private Vector3 originalRotation = new Vector3(0, 0, 0);
    private Vector3 originalScale = new Vector3(0, 0, 0);
    private bool newSizeSet = false;

    [Header("Colors")]
    public Color healColor;
    public Color equipmentColor;
    public Color defaultColor;
    public Color attackColor;
    public Color abilityUsedColor;
    public Color summoningSicknessColor;
    public Color selectedCardColor;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyField = GameObject.Find("EnemyField").GetComponent<EnemyFieldScript>();
    }


    void Update()
    {
        Hover();
        HighlightAbilityOrAttack();

        if (currendCardInHand != gameManager.cardInHand)
        {
            currendCardInHand = gameManager.cardInHand;

            if (currendCardInHand == CardType.Epuipment && gameObject.GetComponent<OneCardManager>().equipmentCount < 4 && gameObject.GetComponent < OneCardManager>().cardAsset.cardType == CardType.Human && CheckIfEquipmentIsPossible())
            {
                //Färben auf equipmentColor
                ChangeColor(equipmentColor, waves);
            }
            else
            {
                //Färben auf defaultColor
                ChangeColor(defaultColor, waves);
            }
        }

        if (currenHighlight != gameManager.highlight)
        {
            currenHighlight = gameManager.highlight;

            if (currenHighlight == Highlight.Heal && gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human && gameObject.GetComponent<OneCardManager>().Healable())
            {
                //Umfärben auf healColor
                ChangeColor(healColor, waves);
            }
            else if (currenHighlight == Highlight.Attack && (gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy || gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Egg))
            {
                if (enemyField.HasTaunt() && gameObject.GetComponent<OneCardManager>().cardAsset.taunt)
                {
                    ChangeColor(attackColor, waves);
                }
                else if (!enemyField.HasTaunt())
                {
                    ChangeColor(attackColor, waves);
                }
                //Umfärben auf attackColor
            }
            else
            {
                //färben auf defaultColor
                ChangeColor(defaultColor, waves);
            }
        }

        if (healAbilityUsed != gameObject.GetComponent<OneCardManager>().HealAbilityAvailible())
        {
            healAbilityUsed = gameObject.GetComponent<OneCardManager>().HealAbilityAvailible();

            if (healAbilityUsed)
            {
                ChangeColor(abilityUsedColor, healAbility);
                ChangeBoxColliderState(false, healAbility);
            }
            else
            {
                ChangeColor(defaultColor, healAbility);
                ChangeBoxColliderState(true, healAbility);
            }
        }

        if (attackAbilityUsed != gameObject.GetComponent<OneCardManager>().AttackAbilityAvailible())
        {
            attackAbilityUsed = gameObject.GetComponent<OneCardManager>().AttackAbilityAvailible();

            if (attackAbilityUsed)
            {
                ChangeColor(abilityUsedColor, attackAbility);
                ChangeBoxColliderState(false, attackAbility);
            }
            else
            {
                ChangeColor(defaultColor, attackAbility);
                ChangeBoxColliderState(true, attackAbility);
            }
        }

        if (summoningSickness != gameObject.GetComponent<OneCardManager>().cardAsset.summoningSickness || attackUsed != gameObject.GetComponent<OneCardManager>().cardAsset.attackUsed)
        {
            summoningSickness = gameObject.GetComponent<OneCardManager>().cardAsset.summoningSickness;
            attackUsed = gameObject.GetComponent<OneCardManager>().cardAsset.attackUsed;

            if (gameObject.GetComponent<OneCardManager>().cardAsset.summoningSickness || gameObject.GetComponent<OneCardManager>().cardAsset.attackUsed)
            {
                ChangeColor(summoningSicknessColor, bigImages);
            }
            else
            {
                ChangeColor(defaultColor, bigImages);
            }
        }
    }

    private void ChangeColor(Color color, List<Image> toChange)
    {
        for (int i = 0; i < toChange.Count; i++)
        {
            toChange[i].GetComponent<Image>().color = color;
        }
    }

    private void ChangeBoxColliderState(bool toSetTo, List<Image> toChange)
    {
        for (int i = 0; i < toChange.Count; i++)
        {
            toChange[i].GetComponent<BoxCollider2D>().enabled = toSetTo;
        }
    }

    private void Hover()
    {
        Vector3 mousePos = Input.mousePosition;

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.name == "HPIcon" && gameObject.GetComponent<OneCardManager>().onBoard)
            {
                hit.collider.GetComponentInParent<HighlightCard>().maxLife.enabled = true;
                hit.collider.GetComponentInParent<HighlightCard>().currentLife.enabled = false;
            }
            else
            {
                hit.collider.GetComponentInParent<HighlightCard>().maxLife.enabled = false;
                hit.collider.GetComponentInParent<HighlightCard>().currentLife.enabled = true;
            }
        }
    }

    private bool CheckIfEquipmentIsPossible()
    {
        CardAsset equipment = gameObject.GetComponent<OneCardManager>().cardAsset;

        if (!gameManager.currentlyDragging.cardAsset.lowHeal.enabled && !gameManager.currentlyDragging.cardAsset.highHeal.enabled && !gameManager.currentlyDragging.cardAsset.lowDMG.enabled && !gameManager.currentlyDragging.cardAsset.highDMG.enabled)
        {
            return true;
        }

        if (gameManager.currentlyDragging.cardAsset.lowHeal.enabled && !equipment.lowHeal.enabled)
        {
            return true;
        }

        if (gameManager.currentlyDragging.cardAsset.highHeal.enabled && !equipment.highHeal.enabled)
        {
            return true;
        }

        if (gameManager.currentlyDragging.cardAsset.lowDMG.enabled && !equipment.lowDMG.enabled)
        {
            return true;
        }

        if (gameManager.currentlyDragging.cardAsset.highDMG.enabled && !equipment.highDMG.enabled)
        {
            return true;
        }

        return false;

    }
    private void HighlightAbilityOrAttack()
    {
        if (currendCardInHand != gameManager.cardInHand)
        {
            currendCardInHand = gameManager.cardInHand;

            if (gameManager.clicked01 == gameObject.GetComponent<OneCardManager>())
            {
                ChangeColor(selectedCardColor, waves);

            }
            else
            {
                ChangeColor(defaultColor, waves);
                return;
            }
            if (gameManager.abilityUser != null)
            {

            }
        }
    }

}
