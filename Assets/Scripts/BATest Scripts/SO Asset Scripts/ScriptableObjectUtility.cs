using UnityEditor;
using UnityEngine;

public static class ScriptableObjectUtility
{
    ///Create new asset from SO type with unique name at
    ///selected folder in project window.
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        var asset = ScriptableObject.CreateInstance<T>();
        ProjectWindowUtil.CreateAsset(asset, "New " + typeof(T).Name + " .asset");
    }
}
