using System;
using System.Runtime.CompilerServices;
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


    //Player Variables
    //Public PlayerEmailType playerEmailType; // Ham or Phising
    //Public PlayerEmailQue playerEmailQue; // eg; 2 3 4

    //Email Variables
    //Public EmailType emailType; // Ham or Phising
    //Public EmailQue emailQue; // eg; 1 3 5

    // Score variables
    private const int scoreCorrect = 100; //Read up on Readonly vs const - which is better in this case
    private const int scoreQueMax = 100; // --depening on method, might remove
    public int score = 0; // Change variable name to more fitting - somthing related to active score for this mail
    public int scoreQue = 0;

    //Other Variables
    public Button button;

    //Temporary variables
    public string variableA = "Ham"; // Placeholder for Player email type
    public string variableB = "Ham"; // Placeholder for Email Type
    public int[] pQue = {1, 0, 1, 0};
    public int[] eQue = {1, 1, 0, 0};
    public int level = 1;
    public int correctQue;
    public int incorrectQue;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        button.onClick.AddListener(CompareAnswer);
    }
    
    // Unsubscribe to prevent strange behavious (according to person on the internet, confirm w head of unity)
    // TEST LATER
    private void OnDestroy() {
        button.onClick.RemoveListener(CompareAnswer);
    }
    
    private void CompareAnswer () {
        if (variableA == variableB) {
            if (level == 1) {
                score = scoreCorrect;
            }
            else if (level > 1) {
                score = (scoreCorrect + (scoreCorrect / 2)) * (level - 1); // Fix math so it is 100 base and if statement not required?
            }
            else { Debug.Log("Error: Level not found"); }

            Debug.Log("Score before quecalc : " + score);

            CalculateCue();
            // Call script that sends the values
        }
        else {
            Debug.Log("A and B are diffrent");
            // End script
        }
    }

    private void CalculateCue() {
        // Check playerEmailQue against emailQue
        Debug.Log("Initilize Calculate Cue");

        for (int i = 0; i < eQue.Length; i++) {
            //Fix according to enum which is correct and not correct
            if (pQue[i] == eQue[i]) {
                Debug.Log("Equal");
                correctQue++;
            }
            else {
                Debug.Log("Incorrect");
                incorrectQue++;
            }
        }

        scoreQue = (scoreQueMax / eQue.Length) * correctQue;
        Debug.Log(scoreQue);
    }

    public void God() {
        Debug.Log("If you’re without sin, cast the first stone – (John 8:7)");
    }
}
