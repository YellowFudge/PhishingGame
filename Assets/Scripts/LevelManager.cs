using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /*All levels is in one scene which means that this keeps track of: 
     * Current mail
     * next mail (all ham/phish mails in level + IT info)
     * Manage day changes (prompt cutscenes to)
     * Manage score scene transition (end)
     * Manage start (tutorial?)
     * 
     * Managed BY EVALUATION INSTEAD: start messages each day (evaluation message based on score previous day)
     */

    //gets responsemail from evaluationsystem (all days but first (+ tutorial?))
    //IT info mail (what to look out for extra today) also from scriptable object list? Can they vary depending on gameplay?
    //takes current day's mails from a scriptable object holding them in order


    /*Tutorial should show player how to:
    * Click phish/ham 
    * Check the cue checkboxes they think a mail that is phishing has
    * where to get all info (infostations) (?)
    */
    public static UnityAction NextMailEvent;

    [SerializeField] DailyMailArrayScriptObj dailyMailsScriptObj;
    [SerializeField] DailyMailArrayScriptObj iTMailsScriptObj;
    [SerializeField] GameObject wizStartMail;
    [SerializeField] Transform mailSpawnPoint;
    [SerializeField] Transform InfoMailSpawnPoint;

    [SerializeField] CutsceneManager cutsceneManager;
    int _currentDay = 1;
    int _currentMail = 0;
    int _amountOfDays; 
    GameObject _currentMailObject; //garbage collector going to hate this?

    public int CurrentDay { get { return _currentDay; } }

    public MailCueTypes GetCurrentMailinfo()
    {
        if(dailyMailsScriptObj.GetCurrentMailinfo(_currentDay, _currentMail, out MailCueTypes mailCues))
        {
            return mailCues;
        }
            
        return null;
    }

private void Start()
    {
        _amountOfDays = dailyMailsScriptObj.GetNumOfDays();
        
        cutsceneManager.StartFirstDayCutScene();

        StartTutorial();


    }

    private void OnEnable()
    {
        //CutsceneManager.EndOfStartDayTriggeredEvent += StartDay;//REMOVE LATER
        CutsceneManager.EndOfScrollThrowTriggeredEvent += StartDay;//REMOVE LATER?
        CutsceneManager.EndOfGoToCreditsTriggeredEvent += GoToCredits;
    }

    private void OnDisable()
    {
        //CutsceneManager.EndOfStartDayTriggeredEvent -= StartDay;//REMOVE LATER
        CutsceneManager.EndOfScrollThrowTriggeredEvent -= StartDay;//REMOVE LATER?
        CutsceneManager.EndOfGoToCreditsTriggeredEvent -= GoToCredits;
    }

    public void StartTutorial()//Special function for day 1? (trigger tutorial? Ask if want tutorial?)
    {
        _currentMailObject = Instantiate(wizStartMail, InfoMailSpawnPoint);
    }

    public void RemoveCurrentMailObject()//UGLY AS HECK REMOVE LATER WHEN REBUILDING BUTTONS
    {
        Destroy(_currentMailObject);
    }

    public void StartDay()
    {
        //Send away info to evaluation (honestly evaluation should subscribe and trigger Start day here instead)

        //send IT info (cannot be sent as ham/spam)
        //TODO:function for that

        //get all mails for the day and present player with first
        NextMail();
    }

    public void ShowITMail()
    {
        //only one itmail per day so only pick first in day's array
        if (!iTMailsScriptObj.GetTodaysMails(_currentDay, out DailyMails mailArray))
        {
            Debug.LogError("There is no IT mail array for this day");
        }

        //destroy old
        Destroy(_currentMailObject);
        _currentMailObject = Instantiate(mailArray.mailPrefabs[0], InfoMailSpawnPoint);
        
    }

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
        
        _currentMailObject = Instantiate(mailObj, mailSpawnPoint);
        NextMailEvent?.Invoke();
    }

    void NextDay()//Starts next day
    {
        _currentDay++;
        _currentMail = 0;

        //if last day -> go to endscene
        if (_currentDay.Equals(_amountOfDays+1))
        {
            RunCreditsAnimation();
            return;
        }

        RunEndOfDayAnimation();
    }

    void GoToCredits()//Changes to statistics scene
    {
        Debug.Log("CREDITS");
        SceneManager.LoadScene(2);
        
    }

    void RunCreditsAnimation()
    {
        cutsceneManager.StartCreditsCutScene();
    }

    void RunEndOfDayAnimation()
    {
        //use current day num to set in graphics
        cutsceneManager.StartEndOfDayCutScene(_currentDay);
    }
}
