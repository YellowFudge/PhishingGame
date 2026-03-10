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
 *
 *
 * NOTES:
 * Replace: VariableA with playerEmailType
 * Replace: VariableB with emailType
 * Email cues can be an array with 1s and zeros to easily compare
 */

/* TODO:
 * 1. Que points system
 * 2. Make it rewarding for clicking right ques
 *  - To prevent people choosing non or all
 * 3. Create score variables
 * 4. Send score variables
 * 5.1 Clean up Debug
 * 5.2 Clean up comments
 * 5.3 Change temporary variables
 */

public class ScoreCalculate : MonoBehaviour
{
    public ScoreScriptableObjectScript scoreScriptableObjectScript;

    // Score variables
    private const int scoreCorrect = 100; //Read up on Readonly vs const - which is better in this case
    private const int scoreQueMax = 100; // --depening on method, might remove
    public float score = 0; // Change variable name to more fitting - somthing related to active score for this 

    //Other Variables
    private Button button;
    private DynamicButtons dynamicButtons;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        //button.onClick.AddListener(StartCalculation);
    }
    
    // Unsubscribe to prevent strange behavious (according to person on the internet, confirm w head of unity)
    // TEST LATER
    private void OnDestroy() {
        //button.onClick.RemoveListener(StartCalculation);
    }

    public void StartCalculation(string playerMailType, string realMailType, List<int> emailQue, List<int> playerQue) {

        //Start calculation
        CompareAnswer(playerMailType, realMailType, emailQue, playerQue);
    }
    
    private void CompareAnswer (string playerMailType, string realMailType, List<int> emailQue, List<int> playerQue) {
        if (playerMailType == realMailType) {
            /* if (level == 1) {
                 score = scoreCorrect;
             }
             else if (level > 1) {
                 score = (scoreCorrect + (scoreCorrect / 2)) * (level - 1); // Fix math so it is 100 base and if statement not required?
             }
             else { Debug.Log("Error: Level not found"); } */

            score = scoreCorrect;

            Debug.Log("Score before quecalc : " + score);

            CalculateCue(emailQue, playerQue);
            // Call script that sends the values
        }
        else {
            Debug.Log("A and B are diffrent");
            // End script
        }
    }

    private float CalculateCue(List<int> emailQue, List<int> playerQue) {
        // Check playerEmailQue against emailQue
        Debug.Log("Initilize Calculate Cue");
        float scoreQue = 0;
        int correctQue = 0;

        for (int i = 0; i < emailQue.Count; i++) {
            //Fix according to enum which is correct and not correct
            if (playerQue[i] == emailQue[i]) {
                Debug.Log("Equal");
                correctQue++;
            }
        }

        scoreQue = (scoreQueMax / emailQue.Count) * correctQue;
        Debug.Log(scoreQue);
        return scoreQue;
    }

    public void God() {
        Debug.Log("If you’re without sin, cast the first stone – (John 8:7)");
    }
}
