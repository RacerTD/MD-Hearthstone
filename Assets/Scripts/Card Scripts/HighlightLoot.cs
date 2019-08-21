using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightLoot : MonoBehaviour
{
    public Color highlightWavesColor;
    public Color defaultWavesColor;
    public GameObject LootWaves;
    public GameManager gameManager;
    
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (gameManager.lootEnabler != null)
        {
                GetComponent<Image>().color = highlightWavesColor;
          
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Image>().color = defaultWavesColor;

            return;
        }
    }
}
