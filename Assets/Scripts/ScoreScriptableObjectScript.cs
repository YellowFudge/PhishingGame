using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScoreScriptableObjectScript", menuName = "Scriptable Objects/ScoreScriptableObjectScript", order = 1)]
public class ScoreScriptableObjectScript : ScriptableObject
{
    public List<PlayerScore> playerScore;
    public double highScore; //Remove once done with new system

    //Mail types chosen
    public int totalCorrectHam;
    public int totalCorrectPhis;
    public int totalWrongHam;
    public int totalWrongPhis;

    public int totalCorrectMail;
    public int totalMail;


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
    public int playerChoseCorrectQue_Error;
    public int playerChoseCorrectQue_SenderDomain;
    public int playerChoseCorrectQue_TooGoodToBeTrue;
    public int playerChoseCorrectQue_GenericGreeting;
    public int playerChoseCorrectQue_LogoImitiation;
    public int playerChoseCorrectQue_NoBranding;
    public int playerChoseCorrectQue_URLOrAttachment;
    public int playerChoseCorrectQue_RequestInfo;
    public int playerChoseCorrectQue_Urgency;
    public int playerChoseCorrectQue_PosesAs;

    //Incorrect ques
    public int playerChoseWrongQue_Error;
    public int playerChoseWrongQue_SenderDomain;
    public int playerChoseWrongQue_TooGoodToBeTrue;
    public int playerChoseWrongQue_GenericGreeting;
    public int playerChoseWrongQue_LogoImitiation;
    public int playerChoseWrongQue_NoBranding;
    public int playerChoseWrongQue_URLOrAttachment;
    public int playerChoseWrongQue_RequestInfo;
    public int playerChoseWrongQue_Urgency;
    public int playerChoseWrongQue_PosesAs;

    //% correct ques
    public float playerPercentRightQue_Error;
    public float playerPercentRightQue_SenderDomain;
    public float playerPercentRightQue_TooGoodToBeTrue;
    public float playerPercentRightQue_GenericGreeting;
    public float playerPercentRightQue_LogoImitiation;
    public float playerPercentRightQue_NoBranding;
    public float playerPercentRightQue_URLOrAttachment;
    public float playerPercentRightQue_RequestInfo;
    public float playerPercentRightQue_Urgency;
    public float playerPercentRightQue_PosesAs;
}


