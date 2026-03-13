using UnityEngine;
using UnityEngine.UI;

public class CalendarCrosses : MonoBehaviour
{
    [SerializeField] GameObject crossDayOne;
    [SerializeField] GameObject crossDayTwo;
    [SerializeField] GameObject crossDayThree;
    int _currentCross;

    private void OnEnable()
    {
        CutsceneManager.EndOfEndDayTriggeredEvent += UpdateCross;
    }

    private void OnDisable()
    {
        CutsceneManager.EndOfEndDayTriggeredEvent -= UpdateCross;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentCross = 0;
        UpdateCross();
        UpdateCross();
    }

    void UpdateCross()
    {
        _currentCross++;
        crossDayOne.SetActive(false);
        crossDayTwo.SetActive(false);
        crossDayThree.SetActive(false);

        if(_currentCross.Equals(1))
        {
            crossDayOne.SetActive(true);
            return;
        }

        if(_currentCross.Equals(2))
        {
            crossDayTwo.SetActive(true);
            return;
        }

        crossDayThree.SetActive(true);
    }


}
