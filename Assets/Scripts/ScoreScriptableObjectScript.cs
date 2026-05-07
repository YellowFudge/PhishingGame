using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScoreScriptableObjectScript", menuName = "Scriptable Objects/ScoreScriptableObjectScript", order = 1)]
public class ScoreScriptableObjectScript : ScriptableObject
{
    public List<PlayerScore> playerScore;
    public double highScore; //Remove once done with new system

    //Mail types chosen
    public int totalCorrectHam = 0;
    public int totalCorrectPhis = 0;
    public int totalWrongHam = 0;
    public int totalWrongPhis = 0;

    public int totalCorrectMail = 0;
    public int totalMail = 0;


    /*
    Error,
    SenderDomain,
    TooGoodToBeTrue,
    GenericGreeting,
    LogoImitiation,
    NoBranding,
    URLOrAttachment,
    RequestInfo,
    Urgency,
    PosesAs
    */

    //Correct ques - Yes this can be made less redundant, but I dont wanna - no time = getto code :)
    public int playerChoseCorrectQue_Error = 0;
    public int playerChoseCorrectQue_SenderDomain = 0;
    public int playerChoseCorrectQue_TooGoodToBeTrue = 0;
    public int playerChoseCorrectQue_GenericGreeting = 0;
    public int playerChoseCorrectQue_LogoImitiation = 0;
    public int playerChoseCorrectQue_NoBranding = 0;
    public int playerChoseCorrectQue_URLOrAttachment = 0;
    public int playerChoseCorrectQue_RequestInfo = 0;
    public int playerChoseCorrectQue_Urgency = 0;
    public int playerChoseCorrectQue_PosesAs = 0;

    //Incorrect ques
    public int playerChoseWrongQue_Error = 0;
    public int playerChoseWrongQue_SenderDomain = 0;
    public int playerChoseWrongQue_TooGoodToBeTrue = 0;
    public int playerChoseWrongQue_GenericGreeting = 0;
    public int playerChoseWrongQue_LogoImitiation = 0;
    public int playerChoseWrongQue_NoBranding = 0;
    public int playerChoseWrongQue_URLOrAttachment = 0;
    public int playerChoseWrongQue_RequestInfo = 0;
    public int playerChoseWrongQue_Urgency = 0;
    public int playerChoseWrongQue_PosesAs = 0;

    //% correct ques
    public float playerPercentRightQue_Error = 0;
    public float playerPercentRightQue_SenderDomain = 0;
    public float playerPercentRightQue_TooGoodToBeTrue = 0;
    public float playerPercentRightQue_GenericGreeting = 0;
    public float playerPercentRightQue_LogoImitiation = 0;
    public float playerPercentRightQue_NoBranding = 0;
    public float playerPercentRightQue_URLOrAttachment = 0;
    public float playerPercentRightQue_RequestInfo = 0;
    public float playerPercentRightQue_Urgency = 0;
    public float playerPercentRightQue_PosesAs = 0;
}


