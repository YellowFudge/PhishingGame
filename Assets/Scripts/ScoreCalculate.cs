using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.Rendering;
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

    //Other Variables
    private Button button;
    private DynamicButtons dynamicButtons;
    

    public void StartCalculation(bool playerMailType, bool realMailType, List<int> emailQue, List<int> playerQue) {
        //Start calculation
        CompareAnswer(playerMailType, realMailType, emailQue, playerQue);
    }
    
    private void CompareAnswer (bool playerMailType, bool realMailType, List<int> emailQue, List<int> playerQue) {
        double playerScore = 0;
        Debug.Log("CompareAnswer");

        if (playerMailType == realMailType) {
            /* if (level == 1) {
                 score = scoreCorrect;
             }
             else if (level > 1) {
                 score = (scoreCorrect + (scoreCorrect / 2)) * (level - 1); // Fix math so it is 100 base and if statement not required?
             }
             else { Debug.Log("Error: Level not found"); } */

            playerScore = scoreCorrect;

            Debug.Log("Score before quecalc : " + playerScore);

            CalculateCue(emailQue, playerQue, playerScore);
        }
        else {
            Debug.Log("A and B are diffrent");
        }
        //Send playerscore
    }

    private double CalculateCue(List<int> emailQue, List<int> playerQue, double playerScore)
    {
        // Check playerEmailQue against emailQue
        Debug.Log("Initilize Calculate Cue");
        int correctQue = 0;

        for (int i = 0; i < emailQue.Count; i++) {
            //Fix according to enum which is correct and not correct
            if (playerQue[i] == emailQue[i]) {
                Debug.Log("Equal");
                correctQue++;
            }
        }

        double scoreQue = (scoreQueMax / emailQue.Count) * correctQue;
        playerScore = playerScore + scoreQue;

        return playerScore;
    }
}
