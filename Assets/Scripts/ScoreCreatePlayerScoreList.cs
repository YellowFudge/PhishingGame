using UnityEditor;
using UnityEngine;

public class ScoreCreatePlayerScoreList {
    [MenuItem("Assets/Create/Score Scriptable Object Script")]
    public static ScoreScriptableObjectScript Create() {
        ScoreScriptableObjectScript asset = ScriptableObject.CreateInstance<ScoreScriptableObjectScript>();

        //AssetDatabase.CreateAsset(asset, "Assets/ScoreScriptableObjectScript.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
