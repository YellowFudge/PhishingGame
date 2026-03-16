using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HamSpamButton : MonoBehaviour
{
    [SerializeField] DynamicButtons dynamicButtons;
    [SerializeField] LevelManager levelManager;
    [SerializeField] ScoreCalculate scoreCalculate;
    [SerializeField] DailyCues dailyCues;
    bool _buttonPressed;

    private void OnEnable()
    {
        LevelManager.NextMailEvent += ResetButtons;
    }

    private void OnDisable()
    {
        LevelManager.NextMailEvent -= ResetButtons;
    }

    void ResetButtons()
    {
        _buttonPressed = false;
    }

    public void SendToScore(bool isSpam)
    {
        if (_buttonPressed) //prevent doubleklicks
        {
            Debug.Log("No");
            return;
        }
        _buttonPressed = true;

        Debug.Log("YEs");
        MailCueTypes mailCue = levelManager.GetCurrentMailinfo();

        //Debug.Log("mailcue is: "+ mailCue);

        //creates list where 1 exists in mail and 0 does not
        bool isChecked;
        List<int> emailCueStates = new List<int>();
        for(int i = 0; i < levelManager.CurrentDay; i++)
        {
            foreach (CueTypes type in (dailyCues.dailyCuesArray[i].cues))
            {
                
                Debug.Log(mailCue.CueTypeArray.Length);
                isChecked = false;
                for (int j = 0; j < mailCue.CueTypeArray.Length; j++)
                {Debug.Log(j);
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
        }
        

        scoreCalculate.StartCalculation(isSpam, mailCue.IsSpamMail, emailCueStates, dynamicButtons.GetResult(), levelManager.CurrentDay);
        levelManager.NextMail(); //calling for next mail
    }
}
