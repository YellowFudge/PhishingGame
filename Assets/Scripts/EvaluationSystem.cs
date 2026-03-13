using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaluationSystem : MonoBehaviour
{
    public ScoreScriptableObjectScript scoreScriptableObjectScript;
    public Button button;

    private void Start() {
        button.onClick.AddListener(CheckCorrectTotal);
    }

    private void OnDestroy() {
        button.onClick.RemoveListener(CheckCorrectTotal);
    }

    public void CheckCorrectTotal() { //change to int?
        // Fetch ScriptObj from ScoreScriptableObjectScript -> PlayerScore -> mailName + isCorrect
        // Loop through all the mails to choose which one to use
        // Temp var, remove
        int dayNum = 1;
        int correctTempVar = 3;
        bool[] arr = new bool[3];
        arr[0] = true;
        arr[1] = true;
        arr[2] = false;
        string mailToReturn;
        int mailVariable;

        List<PlayerScore> playerScore = new List<PlayerScore>();

        Debug.Log("Are we there yet?");
        Debug.Log("Score debug" + playerScore);
        Debug.Log(scoreScriptableObjectScript);
        Debug.Log(scoreScriptableObjectScript.playerScore);

        for (int i = 0; i < scoreScriptableObjectScript.playerScore.Count; i++)
        {
            Debug.Log("Test: " + i);
        }


        int[] test = {1, 1, 1};

        for (int i = 0; i < correctTempVar; i++)
        {
            //Or send new varible, ham vs no ham
            if (arr[i] == false)
            {
                test[i] = i + 4;
            }
            else
            {
                test[i] = i + 1;
            }
            Debug.Log(test[i]);
        }

        mailVariable = test[2]; //Change to random

        mailToReturn = "R" + dayNum + "." + mailVariable;
        //return mailToReturn;
    }
}
