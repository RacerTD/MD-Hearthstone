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
    [MenuItem("Assets/Create/CardAsset", priority = 1)]
    public static void CardAssetObject()
    {
        SOIntegration.CreateMyOwnSO<CardAsset>();
    }
}