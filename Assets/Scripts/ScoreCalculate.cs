
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class ScoreCalculate : MonoBehaviour
{
    
    public ScoreScriptableObjectScript playerResultSO; //SO for storing player results

    //Initiate
    public void StartCalculation(bool playerMailType, bool realMailType /*Enum Variables we need*/) { //Send mailtype + que Enums + Day
        EvaluatePlayerScore(playerMailType, realMailType /*Enum Variables we need*/);
    }

    /// <summary>
    /// Compares the mail type against players selected type - Itterates ScoreScriptableObjectScripts accordingly
    /// </summary>
    private void EvaluatePlayerScore(bool playerMailType, bool realMailType /*Enum Variables we need*/)
    {
        //ScripatbleObj Variables
        PlayerScore newScore = new PlayerScore(); //Instanciate new collection of data for current

        // If player chose phising mail, playerSelectedPhising as "true" so eval can calculate start of day message from boss
        if (playerMailType == true)
        {
            newScore.playerSelectedPhising = true;
        }

        // Main function for comparing if player chose correct mail type
        if (playerMailType == realMailType)
        {
            // Assign value true - important for EvaluationSystem to propely evaluate
            newScore.isCorrect = true;
            scoreScriptableObjectScript.playerScore.Add(newScore);

            if (realMailType == true)
            { // If the mail is phising: Runs que calculation and itterate totalCorrectPhis
                playerResultSO.totalCorrectPhis = playerResultSO.totalCorrectPhis + 1;
                CheckPlayerCorrectQueTypes(/*Enum Variables we need*/);
            } 

            else
            { // Else if the mail is Ham itterate totalCorrectHam
                playerResultSO.totalCorrectHam = playerResultSO.totalCorrectHam + 1;
            }

        } 
        //Comparasion if player chose incorrect mail type
        else if (playerMailType != realMailType) {
            // Assign value false - important for EvaluationSystem to propely evaluate
            newScore.isCorrect = false;
            scoreScriptableObjectScript.playerScore.Add(newScore);

            if (realMailType == true) // If the mail is phising: itterate totalWrongPhis
            {
                playerResultSO.totalWrongPhis = playerResultSO.totalWrongPhis + 1;
            } 

            else
            { // Else if the mail is Ham itterate totalWrongHam
                playerResultSO.totalWrongHam = playerResultSO.totalWrongHam + 1;
            }
        }
    }

    /// <summary>
    /// Compares the mailQueType against player selected queTypes
    /// itterates playerChoseCorrectQue_ or playerChoseWrongQue_ accordingly
    /// </summary>
    private void CheckPlayerCorrectQueTypes(/*Enum Variables we need*/)
    {
        var mailQueTypes = new List<CueTypes> { };
        var playerQueTypes = new List<CueTypes> { };

        // Catch values that are equal in A and B
        // Also catch values that exist in A but not B
        foreach (CueTypes compareType in mailQueTypes)
        {
            if (playerQueTypes.Contains(compareType))
            {
                HandleCorrectCueTypes(compareType);
            }
            else
            {
                HandleWrongCueTypes(compareType);
            }
        }

        // Catch values that exist in B but not A
        foreach (CueTypes compareType in playerQueTypes)
        {
            if (!mailQueTypes.Contains(compareType))
            {
                HandleWrongCueTypes(compareType);
            }
        }
    }

    private void HandleCorrectCueTypes(CueTypes compareType)
    {
        switch (compareType)
        {
            case CueTypes.Error:
                playerResultSO.playerChoseCorrectQue_Error++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.SenderDomain:
                playerResultSO.playerChoseCorrectQue_SenderDomain++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.TooGoodToBeTrue:
                playerResultSO.playerChoseCorrectQue_TooGoodToBeTrue++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.GenericGreeting:
                playerResultSO.playerChoseCorrectQue_GenericGreeting++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.LogoImitiation:
                playerResultSO.playerChoseCorrectQue_LogoImitiation++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.NoBranding:
                playerResultSO.playerChoseCorrectQue_NoBranding++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.URLOrAttachment:
                playerResultSO.playerChoseCorrectQue_URLOrAttachment++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.RequestInfo:
                playerResultSO.playerChoseCorrectQue_RequestInfo++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.Urgency:
                playerResultSO.playerChoseCorrectQue_Urgency++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.PosesAs:
                playerResultSO.playerChoseCorrectQue_PosesAs++;
                break;
        }
    }

    private void HandleWrongCueTypes(CueTypes compareType)
    {
        switch (compareType)
        {
            case CueTypes.Error:
                playerResultSO.playerChoseWrongQue_Error++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.SenderDomain:
                playerResultSO.playerChoseWrongQue_SenderDomain++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.TooGoodToBeTrue:
                playerResultSO.playerChoseWrongQue_TooGoodToBeTrue++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.GenericGreeting:
                playerResultSO.playerChoseWrongQue_GenericGreeting++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.LogoImitiation:
                playerResultSO.playerChoseWrongQue_LogoImitiation++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.NoBranding:
                playerResultSO.playerChoseWrongQue_NoBranding++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.URLOrAttachment:
                playerResultSO.playerChoseWrongQue_URLOrAttachment++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.RequestInfo:
                playerResultSO.playerChoseWrongQue_RequestInfo++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.Urgency:
                playerResultSO.playerChoseWrongQue_Urgency++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.PosesAs:
                playerResultSO.playerChoseWrongQue_PosesAs++;
                break;
        }
    }


    /// <summary>
    /// LEGACY CODE
    /// </summary>

    public ScoreScriptableObjectScript scoreScriptableObjectScript;

    // Score variables
    private const int scoreCorrect = 100;
    private const int scoreQueMax = 100;


    // Initiate calculation
    public void StartCalculation(bool playerMailType, bool realMailType, List<int> emailQue, List<int> playerQue, int dayNum)
    {
        CompareAnswer(playerMailType, realMailType, emailQue, playerQue, dayNum);
    }
    private void CompareAnswer (bool playerMailType, bool realMailType, List<int> emailQue, List<int> playerQue, int dayNum) {
        double playerScore; // --Remove

        // Create new instance for the current mail
        PlayerScore newScore = new PlayerScore();
        //newScore.mailName = "name"; //Change to mail name variable

        // If player chose phising mail, playerSelectedPhising as "true" so eval can calculate start of day message from boss
        if (playerMailType == true) {
            newScore.playerSelectedPhising = true;
        }

        // Check if player mail type is equal to the mail
        // If true, return true to scriptableObj and call que calculation
        // If false, return false to scriptableObj
        if (playerMailType == realMailType) {

            playerScore = scoreCorrect; // --Swap to increment type, eg: Phising correct +1
            newScore.isCorrect = true;
            scoreScriptableObjectScript.playerScore.Add(newScore);

            if (realMailType == true) {
                CalculateCue(emailQue, playerQue, playerScore, dayNum);
            }

            else {
                scoreScriptableObjectScript.highScore = scoreScriptableObjectScript.highScore + playerScore;
            }
        }
        else {
            newScore.isCorrect = false;
            scoreScriptableObjectScript.playerScore.Add(newScore);
            playerScore = 0;
            scoreScriptableObjectScript.highScore = scoreScriptableObjectScript.highScore + playerScore; //Remove when global solutions is fixed
        }
        //scoreScriptableObjectScript.highScore = scoreScriptableObjectScript.highScore + playerScore;
    }

    private double CalculateCue(List<int> emailQue, List<int> playerQue, double playerScore, int dayNum)
    {
        int correctQue = 0;

        for (int i = 0; i < 3 * dayNum; i++) {
            // Fix according to enum which is correct and not correct
            // Temporary Fix
            if (playerQue[i] == emailQue[i]) {
                correctQue++;
            }
        }

        //Debug.Log("Correct: " + correctQue);

        // Change to % calculations
        // Divides the maximum score (100) by the total amount of ques each day
        // and multiply the outcome by total correct ques by players
        double scoreQue = (scoreQueMax / (3 * dayNum)) * correctQue;
        playerScore = playerScore + scoreQue;
        scoreScriptableObjectScript.highScore = scoreScriptableObjectScript.highScore + playerScore;

        // Temp fix, should be playerScore, and player highscore should be calculated in CompareAnswer
        // need to send value back
        return scoreQue; 
    }
}
