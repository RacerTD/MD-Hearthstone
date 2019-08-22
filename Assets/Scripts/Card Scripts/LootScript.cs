using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{

    public AbilityNames thisAbility;

    private LootFildScript lootField;

    private void Awake()
    {
        lootField = GameObject.Find("LootField").GetComponent<LootFildScript>();
    }

    void Start()
    {
        transform.SetParent(lootField.transform, false);
    }

    void Update()
    {
        
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
