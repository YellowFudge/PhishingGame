using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] TMP_Text startOfDayText;
    [SerializeField] PersonManager personManager;
    
    Animator _animator;
    bool _goToCredits = false;
    YarnspinnerManager _yarnspinnerManager;

    public static UnityAction EndOfStartDayTriggeredEvent;
    public static UnityAction EndOfEndDayTriggeredEvent;
    public static UnityAction EndOfGoToCreditsTriggeredEvent;
    public static UnityAction EndOfScrollThrowTriggeredEvent;
    public static UnityAction EndOfFadeAwayBlackTriggeredEvent;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _yarnspinnerManager = GetComponent<YarnspinnerManager>();
    }

    private void OnEnable()
    {
        CutsceneEventManager.ChangePersonEvent += OnChangePerson;
        CutsceneEventManager.SetMoodEvent += OnSetMood;
        CutsceneEventManager.TriggerTalkingEvent += OnTriggerTalk;
        CutsceneEventManager.TriggerExitingEvent += OnTriggerExit;
        CutsceneEventManager.TriggerEnteringEvent += OnTriggerEnter;
        CutsceneEventManager.EndOfITDialougeEvent += OnEndOfITDialouge;
    }

    private void OnDisable()
    {
        CutsceneEventManager.ChangePersonEvent -= OnChangePerson;
        CutsceneEventManager.SetMoodEvent -= OnSetMood;
        CutsceneEventManager.TriggerTalkingEvent -= OnTriggerTalk;
        CutsceneEventManager.TriggerExitingEvent -= OnTriggerExit;
        CutsceneEventManager.TriggerEnteringEvent -= OnTriggerEnter;
        CutsceneEventManager.EndOfITDialougeEvent -= OnEndOfITDialouge;
    }

    /// <summary>
    /// For triggering dialouge cutscenes of a given ID. Currently doesn't check if a node is already running
    /// </summary>
    /// <param name="dialougeID">the title of the node which's cutscene to start</param>
    public void StartDialouge(string dialougeID)
    {
        _yarnspinnerManager.StartDialouge(dialougeID); //Should already be in cutscene when this is called??
    }

    void OnChangePerson(PersonsEnum person, MoodEnum mood)
    {
        //check if cutscene already in action -> otherwise start it
        if (!CutsceneEventManager.inCutscene)
        {
            CutsceneEventManager.inCutscene = true;
            //make background black (should not be? Should always already be in cutscene when this is called?)

        }
        //change it
        personManager.ChangePerson(person, mood);
    }

    void OnSetMood(MoodEnum mood)
    {
        //check if cutscene already in action -> otherwise ignore? (shouldn't be able to start without both mood and person though)
        if (!CutsceneEventManager.inCutscene)
        {
            Debug.LogError("Mood change attempted while not in cutscene. This should not happen");
            return;
        }
        //change it
        personManager.SetMood(mood);
    }

    void OnTriggerTalk()
    {
        //trigger in current person's animator
        personManager.TriggerTalkAnim();
    }

    void OnTriggerExit()
    {
        //trigger in current person's animator
        personManager.TriggerExitAnim();
    }

    void OnTriggerEnter()
    {
        //trigger in current person's animator
        personManager.TriggerEnterAnim();
    }

    void OnEndOfITDialouge() //time to start play (fade away black background)
    {
        //end cutscene
        _animator.SetTrigger("FadeAwayBlack");
        personManager.EndOfDialouge();
        //Fade away black background
        CutsceneEventManager.inCutscene = false;
    }
    

    void SetStartDayTextNum(int newDayNum)
    {
        startOfDayText.text = $"Day\n{newDayNum}";
    }

    public void StartFirstDayCutScene()
    {
        CutsceneEventManager.inCutscene = true;
        SetStartDayTextNum(1);
        _animator.SetTrigger("StartDay");
    }

    void StartThrowScrollsCutScene()
    {
        CutsceneEventManager.inCutscene = true;
        _animator.SetTrigger("ThrowScrolls");
    }

    public void StartEndOfDayCutScene(int NextDayNum)
    {
        CutsceneEventManager.inCutscene = true;
        SetStartDayTextNum(NextDayNum);
        _animator.SetTrigger("EndDay");
    }

    public void StartCreditsCutScene()
    {
        CutsceneEventManager.inCutscene = true;
        _goToCredits = true;
        _animator.SetTrigger("EndDay");
    }

    public void EndOfEndDayCutScene()//triggered by animation
    {
        if (!_goToCredits)
        {
            _animator.SetTrigger("NightToDay");
            EndOfEndDayTriggeredEvent?.Invoke();
            return;
        }
        CutsceneEventManager.inCutscene = false;
        EndOfGoToCreditsTriggeredEvent?.Invoke();
    }

    public void EndOfStartDayCutScene()//triggered by animation
    {
        //not exiting cutscene so not setting inCutscene to false
        EndOfStartDayTriggeredEvent?.Invoke();
    }

    public void EndOfNightToDayCutScene()//triggered by animation
    {
        //not exiting cutscene so not setting inCutscene to false
        _animator.SetTrigger("StartDay");
    }

    public void EndOfFadeAwayBlackCutScene()//triggered by animation
    {
        EndOfFadeAwayBlackTriggeredEvent?.Invoke();
        StartThrowScrollsCutScene();
    }

    public void EndOfThrowScrollsCutScene()//triggered by animation
    {
        CutsceneEventManager.inCutscene = false;
        EndOfScrollThrowTriggeredEvent?.Invoke();
    }
}
