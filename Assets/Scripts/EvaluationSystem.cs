using System;
using System.Security.Cryptography;
using UnityEngine;

public class EvaluationSystem : MonoBehaviour
{
    [SerializeField] Transform MailSpawnPoint;
    [SerializeField] DailyMailArrayScriptObj responseArrayScriptObj;
    public ScoreScriptableObjectScript scoreScriptableObjectScript;
    public CutsceneManager cutsceneManager;
    public LevelManager levelManager;


    private void OnEnable()
    {
        CutsceneManager.EndOfStartDayTriggeredEvent += OnEndDayTriggered;
    }

    private void OnDisable()
    {
        CutsceneManager.EndOfStartDayTriggeredEvent -= OnEndDayTriggered;
    }

    void OnEndDayTriggered() {
        cutsceneManager.StartDialouge(CheckCorrectTotal(levelManager.CurrentDay));
    }

    // Fetch ScriptObj from ScoreScriptableObjectScript -> PlayerScore -> mailName + isCorrect
    // Loop through all the mails to choose which one to use
    public string CheckCorrectTotal(int dayNum) {
        int mailVariable; // Will be assigned by rand
        int[] returnMailNr = { 1, 1, 1 }; // Temp solution, fix later
        int correctTotalTypes = 0;
        int wrongTotalTypes = 0;
        string mailToReturn;

        // Main function for determining return mail value
        // For each itteration
        for (int i = 0; i < 3; i++)
        {
            // Statement counts backwards - to prevent reading the wrong mail with current solution
            // Otherwise the wrong mail will get the wrong return variable
            if (scoreScriptableObjectScript.playerScore[^(i + 1)].playerSelectedPhising == true)
            {
                returnMailNr[i] = 6 - i;
            } else {
                returnMailNr[i] = 3 - i;
            }

            // For counting if all is wrong or if all is right for that day
            if (scoreScriptableObjectScript.playerScore[^(i + 1)].isCorrect == true) {
                correctTotalTypes++;
            } else {
                wrongTotalTypes++;
            }
        }

        //----------------------------------//
        //          With General           //
        //--------------------------------//
        /*
        // Check if all is right/wrong so we know if general message or random message is sent
        if (wrongTotalTypes == 3 || correctTotalTypes == 3) { // All right/wrong
            int goodOrBad = 0;

            if (wrongTotalTypes == 3) {
                goodOrBad = 5;
            }

            mailToReturn = "RG_" + ((dayNum - 1) + goodOrBad);
        } else { // Dispersion, choose random
            var randomBytes = new byte[4];

            // Random function for generating a respons
            // Swap for value based system later, so each response carry a higher or lower value to be choosen
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomBytes);
                uint trueRandom = BitConverter.ToUInt32(randomBytes, 0);

                int randNr = (int)(trueRandom % 3); // 0, 1, or 2

                mailVariable = returnMailNr[randNr];
            }

            // Art is made by god,and I am the artist - Richard 23:13 15-Mar-26
            // Will return "R1.3" for example
            mailToReturn = "R" + (dayNum - 1) + "_" + mailVariable; //Change to _ rather than .
        } */

        //--------------------------------------//
        //          Without General            //
        //------------------------------------//
        
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
        mailToReturn = "R" + (dayNum - 1) + "_" + mailVariable; //Change to _ rather than .
        

        //mailToReturn = "R2_1";
        //Debug.Log("mailToReturn: " + mailToReturn);

        return mailToReturn;
    }
}
