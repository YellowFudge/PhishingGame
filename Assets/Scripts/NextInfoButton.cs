using UnityEngine;
using UnityEngine.UI;

public class NextInfoButton : MonoBehaviour
{
    [SerializeField] Button openITButton;
    [SerializeField] Button startDayButton;
    [SerializeField] GameObject blockingChild;

    private void Start()
    {
        openITButton.gameObject.SetActive(false);
        startDayButton.gameObject.SetActive(false);
        blockingChild.SetActive(false);
    }

    private void OnEnable()
    {
        CutsceneManager.EndOfStartDayTriggeredEvent += ResetInfoObjects;//REMOVE LATER?
    }

    private void OnDisable()
    {
        CutsceneManager.EndOfStartDayTriggeredEvent -= ResetInfoObjects;//REMOVE LATER?
    }

    void ResetInfoObjects()
    {
        openITButton.gameObject.SetActive(true);
        startDayButton.gameObject.SetActive(false);
        blockingChild.SetActive(true);
    }

    public void NextPressed(bool startDay)
    {
        if (startDay) 
        { 
            startDayButton.gameObject.SetActive(false);
            blockingChild.SetActive(false);
            return;
        }

        openITButton.gameObject.SetActive(false);
        startDayButton.gameObject.SetActive(true);
    }
}
