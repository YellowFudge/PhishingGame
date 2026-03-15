using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.Rendering;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

/* CASE 1 - Player press send btn
 * Initiate score sequence
 * 
 * CASE 2 - Player forwards mail
 * System checks Mail variable (Phising or Ham) against anwser (Phsing or Ham)
 * 
 * CASE 3 - Player forwards correct mail to wizard
 * Player is awarded base score for correct Ham/Phising
 * System initiates checkbox calulation (For loop for scalability?)
 * 
 * Case 4 - Score measurment
 * Track: Correct/Incorrect email type, score points gained, 
 * 
 * CASE 5
 * Forward score points gained, correct/incorrect responses 
 * 
 * CASE 6 clear code
 * After function is done, cleanslate the script - prevents score piling from previous.
 */

/* TODO:
 * 1. Send score variables
 * 2. Further develop score
 */

public class ScoreCalculate : MonoBehaviour
{
    public ScoreScriptableObjectScript scoreScriptableObjectScript;

    // Score variables
    private const int scoreCorrect = 100; 
    private const int scoreQueMax = 100;

    // Initiate calculation
    public void StartCalculation(bool playerMailType, bool realMailType, List<int> emailQue, List<int> playerQue, int dayNum) {
        CompareAnswer(playerMailType, realMailType, emailQue, playerQue, dayNum);
    }
    
    private void CompareAnswer (bool playerMailType, bool realMailType, List<int> emailQue, List<int> playerQue, int dayNum) {
        double playerScore;

        // Create new instance for the current mail
        PlayerScore newScore = new PlayerScore();
        newScore.mailName = "name"; //Change to mail name variable

        // Check if player mail type is equal to the mail
        // If true, return true to scriptableObj and call que calculation
        // If false, return false to scriptableObj
        if (playerMailType == realMailType) {
            playerScore = scoreCorrect;

            newScore.isCorrect = true;
            scoreScriptableObjectScript.playerScore.Add(newScore);

            CalculateCue(emailQue, playerQue, playerScore, dayNum);
        }
        else {
            newScore.isCorrect = false;
            scoreScriptableObjectScript.playerScore.Add(newScore);
        }
        //Send playerscore to ScriptObj
    }

    private double CalculateCue(List<int> emailQue, List<int> playerQue, double playerScore, int dayNum)
    {
        int correctQue = 0;

        for (int i = 0; i < 3 * dayNum; i++) {
            // Fix according to enum which is correct and not correct
            // Temporary Fix
            if (playerQue[i] == emailQue[i]) {
                //Debug.Log("Equal");
                correctQue++;
            } /* else {
                Debug.Log("Not Equal");
            } */
        }

        // Temp fix for que score calculation
        // Divides the maximum score (100) by the total amount of ques each day
        // and multiply the outcome by total correct ques by players
        double scoreQue = (scoreQueMax / (3 * dayNum)) * correctQue;
        playerScore = playerScore + scoreQue;   

        return playerScore;
    }
}
