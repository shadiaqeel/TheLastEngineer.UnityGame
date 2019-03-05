using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject {
    [MenuItem("Assets/Create/ScriptableObject/EnemySetting")]
    public static void CreateMyAsset()
    {
        EnemySetting asset = ScriptableObject.CreateInstance<EnemySetting>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObject/EnemySettings/NewEnemySetting.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}