using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreScriptableObjectScript", menuName = "Scriptable Objects/ScoreScriptableObjectScript", order = 1)]
public class ScoreScriptableObjectScript : ScriptableObject
{
    public int highScore;
    public int correctMails;
    public int incorrectMails;
    public int correctQues;
    public int incorrectQues;
    //public List<QueTypeList> queList;
}