
using System.Collections.Generic;
using UnityEngine;

/* TODO:
 * 1. Change Score to %
 * 2. Add enum for ques
 * 3. Create storage for wrong/right response - so they can be compared
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
        //newScore.mailName = "name"; //Change to mail name variable

        if (playerMailType == true)
        {
            newScore.playerSelectedPhising = true;
        }

        // Check if player mail type is equal to the mail
        // If true, return true to scriptableObj and call que calculation
        // If false, return false to scriptableObj
        if (playerMailType == realMailType) {
            playerScore = scoreCorrect;
            newScore.isCorrect = true;
            scoreScriptableObjectScript.playerScore.Add(newScore);

            if (realMailType == true)
            {
                CalculateCue(emailQue, playerQue, playerScore, dayNum);
            }
            //Remove else and move highScore script outside if statement when fixed, temp solution
            else
            {
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
