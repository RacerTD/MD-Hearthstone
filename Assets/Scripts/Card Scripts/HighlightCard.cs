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
    public List<Image> bigImages = new List<Image>();

    public GameManager gameManager;
    public EnemyFieldScript enemyField;
    private bool summoningSickness;
    private CardType currendCardInHand = CardType.Nothing;
    private Highlight currentHighlight = Highlight.Nothing;
    private bool healAbilityUsed = false;
    private bool attackAbilityUsed = false;
    private bool seeMaxLife = false;
    private bool attackUsed = false;
    private bool clickedAbility01 = false;
    private bool clickedAbility02 = false;
    public TextMeshProUGUI maxLife;
    public TextMeshProUGUI currentLife;

    public GameObject lowHealGlow;
    public GameObject highHealGlow;
    public GameObject lowDMGGlow;
    public GameObject highDMGGlow;
    public GameObject enemyWave;

    private bool newSizeSet = false;
    public bool normalLifeEnabled = true;

    [Header("Colors")]
    public Color healColor;
    public Color equipmentColor;
    public Color defaultColor;
    public Color attackColor;
    public Color abilityUsedColor;
    public Color summoningSicknessColor;
    public Color selectedCardColor;
    public Color defaultEnemyColor;
    public Color maxLifeColor;
    public Color currentLifeColor;
    public Color lifeDisabledColor;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyField = GameObject.Find("EnemyField").GetComponent<EnemyFieldScript>();
    }

    private void Attack()
    {
        if (gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy || gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy)
        {
            if (enemyField.HasTaunt() && gameObject.GetComponent<OneCardManager>().cardAsset.taunt)
            {
                ChangeColor(attackColor, waves);
            }
            else if (!enemyField.HasTaunt())
            {
                ChangeColor(attackColor, waves);
            }
        }
        else
        {
            ChangeColor(defaultEnemyColor, waves);
        }
    }

    private void AbilityUser()
    {
        if (gameManager.GetComponent<GameManager>().abilityUser != null)
        {

            if (GetComponent<OneCardManager>().lowHealEnabled == true && gameManager.healAbilityCost != 0 && gameManager.abilityUser == gameObject.GetComponent<OneCardManager>())
            //Highlights low Heal Ability
            {
                lowHealGlow.SetActive(true);
            }

            if (GetComponent<OneCardManager>().highhealEnabled == true && gameManager.healAbilityCost != 0 && gameManager.abilityUser == gameObject.GetComponent<OneCardManager>())
            //Highlights high Heal Ability
            {
                highHealGlow.SetActive(true);
            }

            if (GetComponent<OneCardManager>().lowDMGGameObject == true && gameManager.DMGAbilityCost != 0 && gameManager.abilityUser == gameObject.GetComponent<OneCardManager>())
            //Highlights low Damage Ability
            {
                lowDMGGlow.SetActive(true);
            }

            if (GetComponent<OneCardManager>().highDMGGameObject == true && gameManager.DMGAbilityCost != 0 && gameManager.abilityUser == gameObject.GetComponent<OneCardManager>())
            //Highlights high Damage Ability
            {
                highDMGGlow.SetActive(true);
            }
        }
        else
        {
            lowHealGlow.SetActive(false);
            lowDMGGlow.SetActive(false);
            highDMGGlow.SetActive(false);
            highHealGlow.SetActive(false);
        }
    }

    private void HealAndAttackHighlight()
    {
        if (gameManager.highlight == Highlight.Heal && gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human && gameObject.GetComponent<OneCardManager>().Healable())
        {
            //Umfärben auf healColor
            ChangeColor(healColor, waves);
        }
        else if (gameManager.highlight == Highlight.Attack && (gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy || gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Egg))
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
            enemyWave.GetComponent<Image>().color = defaultEnemyColor;
        }
    }


    private void AttackUser()
    {
        if (currentHighlight == Highlight.Attack && gameManager.clicked01 == gameObject.GetComponent<OneCardManager>())
        {
            //Highlights child when it does a basic attack
            ChangeColor(selectedCardColor, waves);
        }
        else
        {
            ChangeColor(defaultColor, waves);
        }
    }

    private void HealAbilityUsed()
    {
        if (healAbilityUsed != gameObject.GetComponent<OneCardManager>().HealAbilityAvailible())
        {
            healAbilityUsed = gameObject.GetComponent<OneCardManager>().HealAbilityAvailible();

            if (healAbilityUsed)
            {
                ChangeColor(abilityUsedColor, healAbility);
                //ChangeBoxColliderState(false, healAbility);
            }
            else
            {
                ChangeColor(defaultColor, healAbility);
                //ChangeBoxColliderState(true, healAbility);
            }
        }
    }

    private void AttackAbilityUsed()
    {
        if (attackAbilityUsed != gameObject.GetComponent<OneCardManager>().AttackAbilityAvailible())
        {
            attackAbilityUsed = gameObject.GetComponent<OneCardManager>().AttackAbilityAvailible();

            if (attackAbilityUsed)
            {
                ChangeColor(abilityUsedColor, attackAbility);
                //ChangeBoxColliderState(false, attackAbility);
            }
            else
            {
                ChangeColor(defaultColor, attackAbility);
                //ChangeBoxColliderState(true, attackAbility);
            }
        }
    }

    private void SummoningSickness()
    {
        if (gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human && (gameObject.GetComponent<OneCardManager>().summoningSickness || gameObject.GetComponent<OneCardManager>().attackUsed))
        {
            ChangeColor(summoningSicknessColor, bigImages);
        }
        else
        {
            ChangeColor(defaultColor, bigImages);
        }
    }

    private void EquipmentHighlight()
    {
        if (currendCardInHand != gameManager.cardInHand)
        {
            currendCardInHand = gameManager.cardInHand;

            if (currendCardInHand == CardType.Epuipment && gameObject.GetComponent<OneCardManager>().equipmentCount < 4 && gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human && CheckIfEquipmentIsPossible())
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
    }

    void Update()
    {
        //Attack();
        AbilityUser();
        //AttackUser();
        //HealAndAttackHighlight();
        HealAbilityUsed();
        AttackAbilityUsed();
        SummoningSickness();
        //EquipmentHighlight();
        //Hover();

        WavesHighlight();
    }

    private void WavesHighlight()
    {
        if (gameManager.highlight == Highlight.Heal && gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human && gameObject.GetComponent<OneCardManager>().Healable())
        {
            //Umfärben auf healColor
            ChangeColor(healColor, waves);
        }
        else if (gameManager.highlight == Highlight.Attack && (gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy || gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Egg) && (gameManager.clicked01 != null || gameManager.abilityUser != null))
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
        else if (gameManager.highlight == Highlight.Attack && gameManager.clicked01 == gameObject.GetComponent<OneCardManager>())
        {
            //Highlights child when it does a basic attack
            ChangeColor(selectedCardColor, waves);
        }
        else if (gameManager.cardInHand == CardType.Epuipment && gameObject.GetComponent<OneCardManager>().equipmentCount < 4 && gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human)
        {
            //Färben auf equipmentColor
            ChangeColor(equipmentColor, waves);
        }
        else
        {
            if (gameObject.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human)
            {
                ChangeColor(defaultColor, waves);
            }
            else
            {
                ChangeColor(defaultEnemyColor, waves);
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

    public void EnableMaxLife()
    {
        if (normalLifeEnabled)
        {
            normalLifeEnabled = false;
            maxLife.color = maxLifeColor;
            currentLife.color = lifeDisabledColor;
        }
        else
        {
            normalLifeEnabled = true;
            maxLife.color = lifeDisabledColor;
            currentLife.color = currentLifeColor;
        }
    }

    private bool CheckIfEquipmentIsPossible()
    {
        CardAsset equipment = gameObject.GetComponent<OneCardManager>().cardAsset;

        if (!gameManager.currentlyDragging.cardAsset.lowHeal.enabled && !gameManager.currentlyDragging.cardAsset.highHeal.enabled && !gameManager.currentlyDragging.cardAsset.lowDMG.enabled && !gameManager.currentlyDragging.cardAsset.highDMG.enabled)
        {
            return true;
        }
        return false;
    }

}
