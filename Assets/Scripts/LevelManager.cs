using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// All levels is in one scene which means that this keeps track of: 
/// Current mail.
/// next mail (all ham/phish mails in level).
/// Manage day changes (prompt cutscenes for end day).
/// Manage prompting of IT cutscenes.
/// Manage score scene transition (end of game).
/// Manage first day start cutscene (tutorial).
/// </summary>
public class LevelManager : MonoBehaviour
{
    /*Tutorial should show player how to:
    * Click phish/ham 
    * Check the cue checkboxes they think a mail that is phishing has
    * where to get all info (infostations) (?)
    * book
    */
    public static UnityAction NextMailEvent;

    [SerializeField] DailyMailArrayScriptObj dailyMailsScriptObj;
    [SerializeField] StringDayArrayScriptObj iTCutsceneIDArray;
    [SerializeField] string wizStartDialougeID;
    [SerializeField] Transform mailSpawnPoint;
    [SerializeField] Transform desktopTransform;

    [SerializeField] CutsceneManager cutsceneManager;
    int _currentDay = 1;
    int _currentMail = 0;
    int _amountOfDays; 
    GameObject _currentMailObject; //garbage collector going to hate this?

    /// <summary>
    /// 1-based number for which day it is (first day == 1)
    /// </summary>
    public int CurrentDay { get { return _currentDay; } }

    public MailCueTypes GetCurrentMailinfo()//if hamspambuttons are pressed before mail has been instanciated mail will be 0
    {
        if(!_currentMail.Equals(0) && dailyMailsScriptObj.GetCurrentMailinfo(_currentDay, _currentMail, out MailCueTypes mailCues))
        {
            return mailCues;
        }
            
        return null;
    }

    private void Start()
    {
        _amountOfDays = dailyMailsScriptObj.GetNumOfDays();
        
        cutsceneManager.gameObject.SetActive(true); //to avoid the errormessages if it isn't active
        cutsceneManager.StartFirstDayCutScene();
    }

    private void OnEnable()
    {
        CutsceneManager.EndOfStartDayTriggeredEvent += StartTutorial;//REMOVE LATER?
        CutsceneManager.EndOfScrollThrowTriggeredEvent += NextMail;//REMOVE LATER?
        CutsceneManager.EndOfGoToCreditsTriggeredEvent += GoToCredits; 
        CutsceneEventManager.EndOfResponseDialougeEvent += StartITDialouge;
    }

    private void OnDisable()
    {
        CutsceneManager.EndOfScrollThrowTriggeredEvent -= NextMail;//REMOVE LATER?
        CutsceneManager.EndOfGoToCreditsTriggeredEvent -= GoToCredits;
        CutsceneEventManager.EndOfResponseDialougeEvent -= StartITDialouge;
    }

    public void StartTutorial()//Special function for day 1? (trigger tutorial? Ask if want tutorial?)
    {
        cutsceneManager.StartDialouge(wizStartDialougeID);

        CutsceneManager.EndOfStartDayTriggeredEvent -= StartTutorial;//so only cares first day
    }

    void StartITDialouge()
    {
        //only one it cutscene per day so only pick first in day's array
        if (!iTCutsceneIDArray.GetCurrentString(_currentDay - 1, 0, out string cutsceneID))
        {
            Debug.LogError($"{iTCutsceneIDArray} has no IT ID in the requested location: {_currentDay - 1},0");
            return;
        }

        cutsceneManager.StartDialouge(cutsceneID);
    }

    /*public void StartDay()
    {
        //Send away info to evaluation (honestly evaluation should subscribe and trigger Start day here instead)

        //send IT info (cannot be sent as ham/spam)
        //TODO:function for that

        //get all mails for the day and present player with first
        NextMail();
    }*/

    public void NextMail()
    {
        _currentMail++;
        //destroy old
        Destroy(_currentMailObject);

        //if last mail of the day-> go to next
        if (!dailyMailsScriptObj.GetCurrentMail(_currentDay, _currentMail, out GameObject mailObj))
        {
            NextDay();
            return;
        }
        
        _currentMailObject = Instantiate(mailObj, mailSpawnPoint.position, Quaternion.identity, desktopTransform);
        NextMailEvent?.Invoke();
    }

    void NextDay()//Starts next day
    {
        _currentDay++;
        _currentMail = 0;

        //if last day -> go to endscene
        if (_currentDay.Equals(_amountOfDays+1))
        {
            cutsceneManager.StartCreditsCutScene();
            return;
        }

        //use current day num to set in graphics
        cutsceneManager.StartEndOfDayCutScene(_currentDay);
    }

    void GoToCredits()//Changes to statistics scene
    {
        SceneManager.LoadScene(2);
    }
}
