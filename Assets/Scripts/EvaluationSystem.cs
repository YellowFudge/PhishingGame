using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EvaluationSystem : MonoBehaviour
{
    [SerializeField] Transform MailSpawnPoint;
    [SerializeField] DailyMailArrayScriptObj responseArrayScriptObj;
    public ScoreScriptableObjectScript scoreScriptableObjectScript;
    public LevelManager levelManager;
    GameObject _currentMailObject;


    private void OnEnable()
    {
        CutsceneManager.EndOfEndDayTriggeredEvent += OnEndDayTriggered;
    }

    private void OnDisable()
    {
        CutsceneManager.EndOfEndDayTriggeredEvent -= OnEndDayTriggered;
    }

    void OnEndDayTriggered() {
        InstantiateMail(levelManager.CurrentDay);
        //CheckCorrectTotal(levelManager.CurrentDay); 
    }

    public void InstantiateMail(int dayNum)
    {
        string mailIDNum = CheckCorrectTotal(dayNum);
        GameObject findPrefab = FindMailPrefab(mailIDNum);
        //destroy old
        Destroy(_currentMailObject);
        _currentMailObject = Instantiate(findPrefab, MailSpawnPoint);
    }

    public void RemoveCurrentMailObject()
    {
        Destroy(_currentMailObject);
    }

    public GameObject FindMailPrefab(string mailIDNum)
    {
        //only one itmail per day so only pick first in day's array
        if (!responseArrayScriptObj.GetTodaysMails(1, out DailyMails mailArray))
        {
            Debug.LogError("There is no IT mail array for this day");
            return null;
        }

        foreach (GameObject mailPrefab in mailArray.mailPrefabs)
        {
            string idNum = mailPrefab.GetComponent<MailId>().IdName;

            if (idNum == mailIDNum)
            {
                return mailPrefab;
            }
        }

        return null;
    }

    public string CheckCorrectTotal(int dayNum) { //change to int?
        // Fetch ScriptObj from ScoreScriptableObjectScript -> PlayerScore -> mailName + isCorrect
        // Loop through all the mails to choose which one to use
        int mailVariable; // Will be assigned by rand
        int[] returnMailNr = { 1, 1, 1 }; // Temp solution, fix later

        // Main function for determining return mail value
        // For each itteration
        //for (int i = 0; i < scoreScriptableObjectScript.playerScore.Count; i++)

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("pResp" + scoreScriptableObjectScript.playerScore[scoreScriptableObjectScript.playerScore.Count - (i + 1)].playerMailTypeResponse);
            //if (scoreScriptableObjectScript.playerScore[i].playerMailTypeResponse == false)
            if (scoreScriptableObjectScript.playerScore[scoreScriptableObjectScript.playerScore.Count - (i + 1)].playerMailTypeResponse == true)
            {
                returnMailNr[i] = i + 4;
                //returnMailNr[i] = i + 3;
                //Debug.Log("We in the if?");
            } else {
                returnMailNr[i] = i + 1;
                //returnMailNr[i] = i;
                //Debug.Log("We in the else?");
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
        string mailToReturn = "R" + (dayNum - 1) + "." + mailVariable;
        
        Debug.Log("mailToReturn" + mailToReturn);

        return mailToReturn;
    }
}
