using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EvaluationSystem : MonoBehaviour
{
    public ScoreScriptableObjectScript scoreScriptableObjectScript;

    public string CheckCorrectTotal(int dayNum) { //change to int?
        // Fetch ScriptObj from ScoreScriptableObjectScript -> PlayerScore -> mailName + isCorrect
        // Loop through all the mails to choose which one to use
        int mailVariable; // Will be assigned by rand
        int[] returnMailNr = { 1, 1, 1 }; // Temp solution, fix later

        // Main function for determining return mail value
        // For each itteration
        for (int i = 0; i < scoreScriptableObjectScript.playerScore.Count; i++)
        {
            if (scoreScriptableObjectScript.playerScore[i].playerMailTypeResponse == false)
            {
                returnMailNr[i] = i + 4;
            } else {
                returnMailNr[i] = i + 1;
            }
        }

        var randomBytes = new byte[4];

        // Random function for generating a respons
        // Swap for value based system later, so each response carry a higher or lower value to be choosen
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
            uint trueRandom = BitConverter.ToUInt32(randomBytes, 0);

            int randNr = (int)(trueRandom % 3); // 0, 1, or 2

            mailVariable = returnMailNr[randNr];
        }

        // Art is made by god,and I am the artist - Richard 23:13 15-Mar-26
        // Will return "R1.3" for example
        string mailToReturn = "R" + dayNum + "." + mailVariable;
        
        return mailToReturn;
    }
}
