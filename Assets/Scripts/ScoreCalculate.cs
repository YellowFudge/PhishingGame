using System;
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
    //Public PlayerEmailType playerEmailType; //Ham or Phising
    //Public PlayerEmailQue playerEmailQue; // eg; 2 3 4

    //Email Variables
    //Public EmailType emailType; //Ham or Phising
    //Public EmailQue emailQue; //eg; 1 3 5

    //Other Variables
    public Button button;

    //Temporary variables
    public string variableA = "Ham"; //Placeholder for Player email type
    public string variableB = "Ham"; //Placeholder for Email Type
    public int[] pQue = {1, 0, 1, 0};
    public int[] eQue = {1, 1, 0, 0};
    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        button.onClick.AddListener(CompareAnswer);
        Debug.Log("Voidstart");
    }
    
    //Unsubscribe to prevent strange behavious (according to person on the internet, confirm w head of unity)
    private void OnDestroy() {
        button.onClick.RemoveListener(CompareAnswer);
        Debug.Log("VoidDestroy");
    }
    
    public void CompareAnswer () {
        Debug.Log("Initiate CompareAnswer");
        if (variableA == variableB) {
            Debug.Log("A and B are equal");
            //Call god
            //Add points to score
            CalculateCue();
        }
        else {
            Debug.Log("A and B are diffrent");
            //End script
        }
    }

    public void CalculateCue() {
        //Check playerEmailQue against emailQue
        Debug.Log("Initilize Calculate Cue");

        for (int i = 0; i < eQue.Length; i++) {
            Debug.Log("Player ques: " + pQue[i]);
            Console.WriteLine(pQue[i]);
            Debug.Log("Email ques: " + eQue[i]);
            Console.WriteLine(eQue[i]);

            //Calculate
            //  Max point possible divided by Cue siez = Cue points per correct
            //  Make rewards more rewarding 
        }
    }

    public void God() {
        Debug.Log("If you’re without sin, cast the first stone – (John 8:7)");
    }
}
