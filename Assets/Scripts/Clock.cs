using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] Sprite[] clocksInOrder;
    Image _clockImage;
    int _currentSpriteIndex;

    private void OnEnable()
    {
        MailEventManager.ScrollSentEvent += NextImage;
        CutsceneManager.EndOfEndDayTriggeredEvent += ResetClock;
    }

    private void OnDisable()
    {
        MailEventManager.ScrollSentEvent -= NextImage;
        CutsceneManager.EndOfEndDayTriggeredEvent -= ResetClock;
    }

    private void Awake()
    {
        _currentSpriteIndex = 0;
        _clockImage = GetComponent<Image>();
    }

    void ResetClock()
    {
        _currentSpriteIndex = -1;
        NextImage();
    }

    public void NextImage()
    {
        _currentSpriteIndex++;
        if(_currentSpriteIndex <  clocksInOrder.Length)
        {
            _clockImage.sprite = clocksInOrder[_currentSpriteIndex];
        }
        /*else //No warning since last mail of day will always trigger it now?
        {
            Debug.LogError($"Clock is asked to show next image when none exsists. Ensure that the amount of mails per day match the number of clock images.");
        }*/
    }
}
