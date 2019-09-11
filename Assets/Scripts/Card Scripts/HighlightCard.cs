using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using TMPro;

public class HighlightCard : MonoBehaviour
{
    [Header("Things to Color")]
    public List<Image> waves = new List<Image>();
    public List<Image> healAbility = new List<Image>();
    public List<Image> attackAbility = new List<Image>();
    public List<Image> bigImages = new List<Image>();
    public Sprite fullLifeSprite;
    public Sprite lifeSprite;
    public Image lifeBackground;

    [Header("Public Field Stuff")]
    public GameManager gameManager;
    public EnemyFieldScript enemyField;
    public ManaScript mana;

    private CardType currendCardInHand = CardType.Nothing;
    private Highlight currentHighlight = Highlight.Nothing;
    private bool healAbilityUsed = false;
    private bool attackAbilityUsed = false;

    [Header("Life Texts")]
    public TextMeshProUGUI maxLife;
    public TextMeshProUGUI currentLife;

    [Header("Everything Abilitys")]
    public GameObject lowHealInUseGlow;
    public GameObject highHealInUseGlow;
    public GameObject lowDMGInUseGlow;
    public GameObject highDMGInUseGlow;

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
    public Color lowHPColor;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyField = GameObject.Find("EnemyField").GetComponent<EnemyFieldScript>();
        mana = GameObject.Find("Mana").GetComponent<ManaScript>();
    }

    void Update()
    { 
        HealAbilityUsed();
        AttackAbilityUsed();
        SummoningSickness();
        RoundStartHighlight();
        NormalLifeColor();

        WavesHighlight();
    }

    private void RoundStartHighlight()
    {
        if (gameManager.highlightState == HighlightState.RoundStart)
        {
            if (GetComponent<OneCardManager>().lowHealEnabled == true)
            {
                lowHealGlow.SetActive(true);
            }
            else
            {
                lowHealGlow.SetActive(false);
            }

            if (GetComponent<OneCardManager>().highhealEnabled == true)
            {
                highHealGlow.SetActive(true);
            }
            else
            {
                highHealGlow.SetActive(false);
            }

            if (GetComponent<OneCardManager>().lowDamageEnabled == true)
            {
                lowDMGGlow.SetActive(true);
            }
            else
            {
                lowDMGGlow.SetActive(false);
            }

            if (GetComponent<OneCardManager>().highDamageEnabled == true)
            {
                highDMGGlow.SetActive(true);
            }
            else
            {
                highHealGlow.SetActive(false);
            }
        }
        else if (gameManager.highlightState == HighlightState.Normal)
        {
            if (GetComponent<OneCardManager>().lowHealEnabled == true && GetComponent<OneCardManager>().cardAsset.lowHeal.cost <= mana.manaCount && GetComponent<OneCardManager>().lowHealUsed == false)
            {
                lowHealGlow.SetActive(true);
            }
            else
            {
                lowHealGlow.SetActive(false);
            }

            if (GetComponent<OneCardManager>().highhealEnabled == true && GetComponent<OneCardManager>().cardAsset.highHeal.cost <= mana.manaCount && GetComponent<OneCardManager>().highHealUsed == false)
            {
                highHealGlow.SetActive(true);
            }
            else
            {
                highHealGlow.SetActive(false);
            }

            if (GetComponent<OneCardManager>().lowDamageEnabled == true && GetComponent<OneCardManager>().cardAsset.lowDMG.cost <= mana.manaCount && GetComponent<OneCardManager>().lowDamageUsed == false)
            {
                lowDMGGlow.SetActive(true);
            }
            else
            {
                lowDMGGlow.SetActive(false);
            }

            if (GetComponent<OneCardManager>().highDamageEnabled == true && GetComponent<OneCardManager>().cardAsset.highDMG.cost <= mana.manaCount && GetComponent<OneCardManager>().highDamageUsed == false)
            {
                highDMGGlow.SetActive(true);
            }
            else
            {
                highDMGGlow.SetActive(false);
            }
        }
        else
        {
            lowHealGlow.SetActive(false);
            lowDMGGlow.SetActive(false);
            highDMGGlow.SetActive(false);
            highHealGlow.SetActive(false);
        }

        AbilityUser();
    }

    private void AbilityUser()
    {
        if (gameManager.GetComponent<GameManager>().abilityUser != null)
        {

            if (GetComponent<OneCardManager>().lowHealEnabled == true && gameManager.healAbilityCost != 0 && gameManager.abilityUser == gameObject.GetComponent<OneCardManager>())
            //Highlights low Heal Ability
            {
                lowHealInUseGlow.SetActive(true);
                lowHealGlow.SetActive(false);
            }

            if (GetComponent<OneCardManager>().highhealEnabled == true && gameManager.healAbilityCost != 0 && gameManager.abilityUser == gameObject.GetComponent<OneCardManager>())
            //Highlights high Heal Ability
            {
                highHealInUseGlow.SetActive(true);
                highHealGlow.SetActive(false);
            }

            if (GetComponent<OneCardManager>().lowDamageEnabled == true && gameManager.DMGAbilityCost != 0 && gameManager.abilityUser == gameObject.GetComponent<OneCardManager>())
            //Highlights low Damage Ability
            {
                lowDMGInUseGlow.SetActive(true);
                lowDMGGlow.SetActive(false);
            }

            if (GetComponent<OneCardManager>().highDamageEnabled == true && gameManager.DMGAbilityCost != 0 && gameManager.abilityUser == gameObject.GetComponent<OneCardManager>())
            //Highlights high Damage Ability
            {
                highDMGInUseGlow.SetActive(true);
                highDMGGlow.SetActive(false);
            }
        }
        else
        {
            lowHealInUseGlow.SetActive(false);
            lowDMGInUseGlow.SetActive(false);
            highDMGInUseGlow.SetActive(false);
            highHealInUseGlow.SetActive(false);
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

    private void NormalLifeColor()
    {
        if (normalLifeEnabled)
        {
            if (gameObject.GetComponent<OneCardManager>().Health < gameObject.GetComponent<OneCardManager>().maxHealth)
            {
                currentLife.color = lowHPColor;
            }
            else
            {
                currentLife.color = currentLifeColor;
            }
        }
        else
        {
            currentLife.color = lifeDisabledColor;
        }

        if (gameObject.GetComponent<OneCardManager>().Health < gameObject.GetComponent<OneCardManager>().maxHealth)
        {
            lifeBackground.sprite = lifeSprite;
        }
        else
        {
            lifeBackground.sprite = fullLifeSprite;
        }

    }
}
