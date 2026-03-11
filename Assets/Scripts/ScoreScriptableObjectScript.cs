using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScoreScriptableObjectScript", menuName = "Scriptable Objects/ScoreScriptableObjectScript", order = 1)]
public class ScoreScriptableObjectScript : ScriptableObject
{
    public List<PlayerScore> playerScore;
}