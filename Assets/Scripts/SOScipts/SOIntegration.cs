using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SOIntegration
{
    public static void CreateMyOwnSO<T>() where T : ScriptableObject
    {
        var asset = ScriptableObject.CreateInstance<T>();
        ProjectWindowUtil.CreateAsset(asset, "new" + typeof(T).Name + ".asset");
    }
}

public class SOEnabler
{
    [MenuItem("Assets/Create/EquipmentCardAsset", priority = 1)]
    public static void EquipmentAssetObject()
    {
        SOIntegration.CreateMyOwnSO<EquipmentCardAsset>();
    }
    [MenuItem("Assets/Create/SpellCardAsset", priority = 1)]
    public static void SpellAssetObject()
    {
        SOIntegration.CreateMyOwnSO<SpellCardAsset>();
    }
    [MenuItem("Assets/Create/EnemyCardAsset", priority = 1)]
    public static void EnemyAssetObject()
    {
        SOIntegration.CreateMyOwnSO<EnemyCardAsset>();
    }
    [MenuItem("Assets/Create/HumanCardAsset", priority = 1)]
    public static void HumanAssetObject()
    {
        SOIntegration.CreateMyOwnSO<HumanCardAsset>();
    }
}