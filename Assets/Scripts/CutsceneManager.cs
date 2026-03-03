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

    public static UnityAction EndOfStartDayTriggered;
    public static UnityAction EndOfGoToCreditsTriggered;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
        EndOfGoToCreditsTriggered?.Invoke();
    }

    public void EndOfStartDayCutScene()//triggered by animation
    {
        EndOfStartDayTriggered?.Invoke();
    }
}