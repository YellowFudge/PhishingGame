using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HamSpamButton : MonoBehaviour
{
    [SerializeField] DynamicButtons dynamicButtons;
    [SerializeField] LevelManager levelManager;
    [SerializeField] ScoreCalculate scoreCalculate;
    [SerializeField] bool isSpamButton;


    public void SendToScore()
    {
        MailCueTypes mailCue = levelManager.GetCurrentMailinfo();

        //creates list where 1 exists in mail and 0 does not
        bool isChecked;
        List<int> emailCueStates = new List<int>();
        foreach (CueTypes type in (CueTypes[])System.Enum.GetValues(typeof(CueTypes)))
        {
            isChecked = false;
            for(int i = 0; i < mailCue.CueTypeArray.Length; i++)
            {
                if (type.Equals(mailCue.CueTypeArray[i]))
                {
                    emailCueStates.Add(1);
                    isChecked = true;
                    break;
                }
            }
            if (!isChecked)
            {
                emailCueStates.Add(0);
            }
        }

        scoreCalculate.StartCalculation(isSpamButton, mailCue.IsSpamMail, emailCueStates, dynamicButtons.GetResult());
    }
}
