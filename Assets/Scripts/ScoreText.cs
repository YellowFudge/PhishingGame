using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    [SerializeField] ScoreScriptableObjectScript scoreScriptObj;
    TMP_Text _textAsset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textAsset = GetComponent<TMP_Text>();
        _textAsset.text = $"Score: {scoreScriptObj.highScore}";
    }
}
