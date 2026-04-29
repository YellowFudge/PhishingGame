using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HamSpamButton : MonoBehaviour //remake into StampsManager. Keeps track of their cover but not stamps in themselves?
{
    [SerializeField] DynamicButtons dynamicButtons;
    [SerializeField] LevelManager levelManager;
    [SerializeField] ScoreCalculate scoreCalculate;
    [SerializeField] DailyCues dailyCues;
    bool _buttonPressed;

    //get current mail object in levelmanager and check when has been opened at least once (make into event this listenes for instead?)
    //open case for stamps when has opened once? but can only stamp on closed? or do you have to deal with your consequenses?
    //

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
        dynamicButtons.ResetToggles();
    }

    public void SendToScore(bool isSpam)
    {
        if (_buttonPressed) //prevent doubleklicks
        {
            //Debug.Log("No");
            return;
        }
        _buttonPressed = true;

        MailCueTypes mailCue = levelManager.GetCurrentMailinfo();

        if (mailCue == null)
        {
            _buttonPressed = false;
            return;
        }//Debug.Log("YEs");

        //Debug.Log("mailcue is: "+ mailCue);

        //creates list where 1 exists in mail and 0 does not
        bool isChecked;
        List<int> emailCueStates = new List<int>();
        for(int i = 0; i < levelManager.CurrentDay; i++)
        {
            foreach (CueTypes type in (dailyCues.dailyCuesArray[i].cues))
            {
                isChecked = false;
                for (int j = 0; j < mailCue.CueTypeArray.Length; j++)
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
        }
        

        scoreCalculate.StartCalculation(isSpam, mailCue.IsSpamMail, emailCueStates, dynamicButtons.GetResult(), levelManager.CurrentDay);
        levelManager.NextMail(); //calling for next mail
    }
}
