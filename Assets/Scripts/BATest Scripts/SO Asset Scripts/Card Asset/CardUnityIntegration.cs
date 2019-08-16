using UnityEditor;
using UnityEngine;


/// <summary>
/// Specifies the path to Asset (as Attribute) and access to 
/// ScriptableObjectUtility to create an Asset with the name <CardAsset>
/// </summary>

static class CardUnityIntegration
{
    //[MenuItem("Assets/Create/CardAsset")]
    public static void CreateYourScriptableObject()
    {
        ScriptableObjectUtility.CreateAsset<CardAsset>();
    }
}
