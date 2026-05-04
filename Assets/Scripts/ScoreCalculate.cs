
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class ScoreCalculate : MonoBehaviour
{
    
    public ScoreScriptableObjectScript scoreScriptableObjectScript; //SO for storing player results

    //Initiate
    public void StartCalculation(bool playerMailType, bool realMailType, CueTypes[] emailCue, CueTypes[] playerCue) { //Send mailtype + que Enums + Day
        EvaluatePlayerScore(playerMailType, realMailType, emailCue, playerCue);
    }

    /// <summary>
    /// Compares the mail type against players selected type - Itterates ScoreScriptableObjectScripts accordingly
    /// </summary>
    private void EvaluatePlayerScore(bool playerMailType, bool realMailType, CueTypes[] emailCue, CueTypes[] playerCue)
    {
        //ScripatbleObj Variables
        PlayerScore newScore = new PlayerScore(); //Instanciate new collection of data for current

        // If player chose phising mail, playerSelectedPhising as "true" so eval can calculate start of day message from boss
        if (playerMailType == true)
        {
            Debug.Log("Phising true");
            newScore.playerSelectedPhising = true;
        }

        // Main function for comparing if player chose correct mail type
        if (playerMailType == realMailType)
        {
            // Assign value true - important for EvaluationSystem to propely evaluate
            newScore.isCorrect = true;
            scoreScriptableObjectScript.playerScore.Add(newScore);
            Debug.Log("entered playerMailType == realMailType");

            if (realMailType == true)
            { // If the mail is phising: Runs que calculation and itterate totalCorrectPhis
                scoreScriptableObjectScript.totalCorrectPhis = scoreScriptableObjectScript.totalCorrectPhis + 1;
                CheckPlayerCorrectQueTypes(emailCue, playerCue);
                Debug.Log("entered If the mail is phising");
            } 

            else
            { // Else if the mail is Ham itterate totalCorrectHam
                scoreScriptableObjectScript.totalCorrectHam = scoreScriptableObjectScript.totalCorrectHam + 1;
            }

        } 
        //Comparasion if player chose incorrect mail type
        else if (playerMailType != realMailType) {
            // Assign value false - important for EvaluationSystem to propely evaluate
            newScore.isCorrect = false;
            scoreScriptableObjectScript.playerScore.Add(newScore);

            if (realMailType == true) // If the mail is phising: itterate totalWrongPhis
            {
                scoreScriptableObjectScript.totalWrongPhis = scoreScriptableObjectScript.totalWrongPhis + 1;
            } 

            else
            { // Else if the mail is Ham itterate totalWrongHam
                scoreScriptableObjectScript.totalWrongHam = scoreScriptableObjectScript.totalWrongHam + 1;
            }
        }
        //Debug.Log("playerChoseCorrectQue_Error: " + scoreScriptableObjectScript.playerChoseCorrectQue_Error);
        //Debug.Log($"playerChoseCorrectQue_Error {scoreScriptableObjectScript.playerChoseWrongQue_Error}");
        //Debug.Log($"scoreScriptableObjectScript.totalCorrectHam {scoreScriptableObjectScript.totalCorrectHam}");
        //Debug.Log($"scoreScriptableObjectScript.totalCorrectHam {scoreScriptableObjectScript.totalCorrectPhis}");
    }

    /// <summary>
    /// Compares the mailQueType against player selected queTypes
    /// itterates playerChoseCorrectQue_ or playerChoseWrongQue_ accordingly
    /// </summary>
    private void CheckPlayerCorrectQueTypes(CueTypes[] emailCue, CueTypes[] playerCue)
    {
        // Catch values that are equal in A and B
        // Also catch values that exist in A but not B
        Debug.Log("entered que check");
        foreach (CueTypes compareType in emailCue)
        {
            if (playerCue.Contains(compareType))
            {
                HandleCorrectCueTypes(compareType);
            }
            else
            {
                HandleWrongCueTypes(compareType);
            }
        }

        // Catch values that exist in B but not A
        foreach (CueTypes compareType in playerCue)
        {
            if (!emailCue.Contains(compareType))
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
                scoreScriptableObjectScript.playerChoseCorrectQue_Error++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.SenderDomain:
                scoreScriptableObjectScript.playerChoseCorrectQue_SenderDomain++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.TooGoodToBeTrue:
                scoreScriptableObjectScript.playerChoseCorrectQue_TooGoodToBeTrue++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.GenericGreeting:
                scoreScriptableObjectScript.playerChoseCorrectQue_GenericGreeting++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.LogoImitiation:
                scoreScriptableObjectScript.playerChoseCorrectQue_LogoImitiation++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.NoBranding:
                scoreScriptableObjectScript.playerChoseCorrectQue_NoBranding++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.URLOrAttachment:
                scoreScriptableObjectScript.playerChoseCorrectQue_URLOrAttachment++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.RequestInfo:
                scoreScriptableObjectScript.playerChoseCorrectQue_RequestInfo++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.Urgency:
                scoreScriptableObjectScript.playerChoseCorrectQue_Urgency++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.PosesAs:
                scoreScriptableObjectScript.playerChoseCorrectQue_PosesAs++;
                break;
        }
    }

    private void HandleWrongCueTypes(CueTypes compareType)
    {
        switch (compareType)
        {
            case CueTypes.Error:
                scoreScriptableObjectScript.playerChoseWrongQue_Error++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.SenderDomain:
                scoreScriptableObjectScript.playerChoseWrongQue_SenderDomain++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.TooGoodToBeTrue:
                scoreScriptableObjectScript.playerChoseWrongQue_TooGoodToBeTrue++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.GenericGreeting:
                scoreScriptableObjectScript.playerChoseWrongQue_GenericGreeting++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.LogoImitiation:
                scoreScriptableObjectScript.playerChoseWrongQue_LogoImitiation++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.NoBranding:
                scoreScriptableObjectScript.playerChoseWrongQue_NoBranding++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.URLOrAttachment:
                scoreScriptableObjectScript.playerChoseWrongQue_URLOrAttachment++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.RequestInfo:
                scoreScriptableObjectScript.playerChoseWrongQue_RequestInfo++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.Urgency:
                scoreScriptableObjectScript.playerChoseWrongQue_Urgency++;
                break;
        }
        switch (compareType)
        {
            case CueTypes.PosesAs:
                scoreScriptableObjectScript.playerChoseWrongQue_PosesAs++;
                break;
        }
    }

    /*
    /// <summary>
    /// LEGACY CODE
    /// </summary>

    public ScoreScriptableObjectScript scoreScriptableObjectScript;

    // Score variables
    private const int scoreCorrect = 100;
    private const int scoreQueMax = 100;


    // Initiate calculation
    public void LegacyStartCalculation(bool playerMailType, bool realMailType, List<int> emailQue, List<int> playerQue, int dayNum)
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
    }*/
}
