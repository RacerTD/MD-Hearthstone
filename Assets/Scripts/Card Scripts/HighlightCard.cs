using System.Collections;
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
    public GameObject gameManager;
    public GameObject enemyField;
    private CardType currendCardInHand = CardType.Nothing;
    private Highlight currenHighlight = Highlight.Nothing;
    private bool healAbilityUsed = false;
    private bool attackAbilityUsed = false;
    private bool seeMaxLife = false;
    public TextMeshProUGUI maxLife;
    public TextMeshProUGUI currentLife;

    [Header("Colors")]
    public Color healColor;
    public Color equipmentColor;
    public Color defaultColor;
    public Color attackColor;

    public Color abilityUsed;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        enemyField = GameObject.Find("EnemyField");
    }


    void Update()
    {
        MaxLifeSwitcher();

        if (currendCardInHand != gameManager.GetComponent<GameManager>().cardInHand)
        {
            currendCardInHand = gameManager.GetComponent<GameManager>().cardInHand;

            if (currendCardInHand == CardType.Epuipment && gameObject.GetComponent<OneCardManager>().equipmentCount <= 4 && gameObject.GetComponent < OneCardManager>().cardAsset.cardType == CardType.Human)
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

        if (currenHighlight != gameManager.GetComponent<GameManager>().highlight)
        {
            currenHighlight = gameManager.GetComponent<GameManager>().highlight;

            if (currenHighlight == Highlight.Heal && gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human)
            {
                //Umfärben auf healColor
                ChangeColor(healColor, waves);
            }
            else if (currenHighlight == Highlight.Attack && gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy)
            {
                if (enemyField.GetComponent<EnemyFieldScript>().taunt && gameObject.GetComponent<OneCardManager>().cardAsset.taunt)
                {
                    ChangeColor(attackColor, waves);
                }
                else if (!enemyField.GetComponent<EnemyFieldScript>().taunt)
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
                ChangeColor(abilityUsed, healAbility);
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
                ChangeColor(abilityUsed, attackAbility);
                ChangeBoxColliderState(false, attackAbility);
            }
            else
            {
                ChangeColor(defaultColor, attackAbility);
                ChangeBoxColliderState(true, attackAbility);
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

    private void MaxLifeSwitcher()
    {
        Vector3 mousePos = Input.mousePosition;

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.name == "HPIcon")
            {
                maxLife.enabled = true;
                currentLife.enabled = false;
            }
            else
            {
                maxLife.enabled = false;
                currentLife.enabled = true;
            }
        }
    }
}
