using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] TMP_Text startOfDayText;
    Animator _animator;
    bool _goToCredits = false;

    public static UnityAction EndOfStartDayTriggeredEvent;
    public static UnityAction EndOfGoToCreditsTriggeredEvent;
    public static UnityAction EndOfScrollThrowTriggeredEvent;


    
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void SetStartDayTextNum(int newDayNum)
    {
        startOfDayText.text = $"Day\n{newDayNum}";
    }

    public void StartFirstDayCutScene()
    {
        SetStartDayTextNum(1);
        _animator.SetTrigger("StartDay");
    }

    public void StartThrowScrollsCutScene()
    {
        _animator.SetTrigger("ThrowScrolls");
    }

    public void StartEndOfDayCutScene(int NextDayNum)
    {
        SetStartDayTextNum(NextDayNum);
        _animator.SetTrigger("EndDay");
    }

    public void StartCreditsCutScene()
    {
        _goToCredits = true;
        _animator.SetTrigger("EndDay");
    }

    public void EndOfEndDayCutScene()//triggered by animation
    {
        if (!_goToCredits)
        {
            _animator.SetTrigger("StartDay");
            return;
        }
        EndOfGoToCreditsTriggeredEvent?.Invoke();
    }

    public void EndOfStartDayCutScene()//triggered by animation
    {
        EndOfStartDayTriggeredEvent?.Invoke();
    }

    public void EndOfThrowScrollsCutScene()//triggered by animation
    {
        EndOfScrollThrowTriggeredEvent?.Invoke();
    }
}