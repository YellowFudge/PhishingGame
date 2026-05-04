using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class ScrollChute : MonoBehaviour, IPointerClickHandler
{
    /* to send letter: open letter [triggers seal cover to retract] 
     *  > stamp with seal(ham/spam)[letter automatically closes when picking up stamps] 
     *  > [if not open]letter send area automatically opens once has stamped 
     *  > place letter in area[automatically closes letter if is open] 
     *  > close send area

    Can always open checklist, follows the letter if sends phishing, otherwise closes and opens again without old markings. 
    Must be closed letter stamped, risks covering text otherwise*/


    //checks if mail has been stamped (asks stamps if they've been used) if yes it sets this as parent and tells scroll to close
    //can now be closed to send away letter (click)
    //if scrolls tells it that it is being moved again: set back to parent in levelhandler 
    //can no longer send letter when closing this (until scroll is inside again).
    //Does not automatically close though (unless is first time taking out letter).


    //scroll created in levelmanager,
    //levelmanager sending event for this to run opening animation
    //mailOpenCloseHandler telling this when it is being moved out from it/clicked via function. IT CAN'T IT IS GENERATED FROM PREFAB
    //      . First time (for this letter) it runs close animation.
    //player stamps scroll, stamps send to this to run openanim through event (has bool that checks if has been told to open?)
    //immediatly check if mail middle area (so not just a corner?) is above chute -> if yes skip next step
    //mail tells chute when dropped on top of it, chute tells it if is allowed to be put back in chute
    // if is allowed to be put in chute -> put chute as parent and send to mail to close
    //if player closes chute -> tell cuelist to run move to chute anim
    //      When done: run close anim and at end send to levelmanager that next mail is needed. Reset all values related to last letter
    //      When done: start over from point 2 with new letter
    //if scrolls tells it that it is being moved again: set back to parent in levelhandler 
    //      can no longer send letter when closing chute (until scroll is inside again).
    //      Does not automatically close though (unless is first time taking out letter).

    [SerializeField] DynamicButtons dynamicButtons;
    [SerializeField] LevelManager levelManager;
    [SerializeField] ScoreCalculate scoreCalculate;
    [SerializeField] DailyCues dailyCues;
    [SerializeField] MailStampManager mailStampManager;
    Animator _animator;//in case is not on chute object?
    bool _hasclosedFirstTime; //automatically closes first time takes out scroll
    bool _isOpen;
    bool _sendingScroll;

    private void OnEnable()
    {
        _isOpen = false;
        _hasclosedFirstTime = false;
        _sendingScroll = false;
        //subscribe to events
        MailEventManager.ScrollStampedEvent += StartOpenAnim;
        LevelManager.NextMailEvent += StartOpenAnim;
        MailEventManager.ScrollLeaveChuteEvent += OnScrollLeaveChute;
        MailEventManager.TryPlaceScrollInChuteEvent += OnTryPlaceScrollInChute;
    }

    private void OnDisable()
    {
        //unsubscribe to events
        MailEventManager.ScrollStampedEvent -= StartOpenAnim;
        LevelManager.NextMailEvent -= StartOpenAnim;
        MailEventManager.ScrollLeaveChuteEvent -= OnScrollLeaveChute;
        MailEventManager.TryPlaceScrollInChuteEvent -= OnTryPlaceScrollInChute;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnScrollLeaveChute(GameObject scrollGO)
    {
        scrollGO.transform.SetParent(levelManager.DesktopTransform);
        StartCloseAnim();
    }

    void OnTryPlaceScrollInChute (GameObject scrollGO)
    {
        if (!_isOpen) return;


        if (mailStampManager.HasStamped)
        {
            //if succeeds/can place in chute (mail is stamped)
            _sendingScroll = true;
            scrollGO.transform.SetParent(levelManager.MailSpawnPointTransform);//in case this is parent
            scrollGO.transform.position = levelManager.MailSpawnPointTransform.position;
            MailEventManager.ScrollPlacedInChuteEvent?.Invoke();
        }

        MailOpenCloseHandler mailOpenClose = scrollGO.GetComponent<MailOpenCloseHandler>();
        MailEventManager.ScrollNotPlacedInChuteEvent?.Invoke(mailOpenClose.PlayerHasOpened);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeOpenCloseState();
    }

    void ChangeOpenCloseState()//accessed via being clicked on
    {
        if(_isOpen)
        {
            StartCloseAnim();
        }
        else
        {
            StartOpenAnim();
        }
    }

    void StartOpenAnim()
    {
        if (_isOpen || _sendingScroll) //if sending scroll you cannot interact with it until done (no regretting mid send)
        {
            return;
        }
        _isOpen = true;
        _animator.SetTrigger("Open");
    }

    void StartCloseAnim()
    {
        if (!_isOpen)
        {
            return;
        }
        if (_sendingScroll)
        {
            MailEventManager.SendingScrollEvent?.Invoke();
        }

        _isOpen = false;
        _animator.SetTrigger("Close");
    }

    public void EndOfOpenAnimTriggered()//called by animation event
    {
        _isOpen = true;

    }

    public void EndOfCloseAnimTriggered()//called by animation event
    {
        _isOpen = false;

        if (_sendingScroll)
        {
            _sendingScroll = false;
            //send to Levelmanager to trigger next mail (hamspambutton replacement here)
            //      get from stamps or mail if player marked it as ham or spam
            SendToScore(mailStampManager.StampedAsPhishing);
            //reset all values to prepare for next mail
            _hasclosedFirstTime = false;
            
            MailEventManager.ScrollSentEvent?.Invoke();
            return;
        }

        if (!_hasclosedFirstTime)
        {
            _hasclosedFirstTime = true;
        }

    }


    public void SendToScore(bool isSpam)
    {
        MailCueTypes mailCue = levelManager.GetCurrentMailinfo();
        
        //UNCOMMENT BELOW AND IT SHOULD WORK !!!! :)
        scoreCalculate.StartCalculation(isSpam, mailCue.IsSpamMail, mailCue.CueTypeArray, dynamicButtons.ConvertEnumToList().ToArray());
        levelManager.NextMail(); //calling for next mail
        dynamicButtons.ResetToggles(); // calling for toggle reset
    }
}
