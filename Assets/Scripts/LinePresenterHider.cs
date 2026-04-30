using UnityEngine;

public class LinePresenterHider : MonoBehaviour
{
    [SerializeField] GameObject linePresenterObject;

    private void OnEnable()
    {
        CutsceneManager.EndOfFadeAwayBlackTriggeredEvent += HideLinePresenter;
        CutsceneManager.EndOfStartDayTriggeredEvent += ShowLinePresenter;
    }

    private void OnDisable()
    {
        CutsceneManager.EndOfFadeAwayBlackTriggeredEvent -= HideLinePresenter;
        CutsceneManager.EndOfStartDayTriggeredEvent -= ShowLinePresenter;
    }

    void HideLinePresenter()
    {
        linePresenterObject.SetActive(false);
    }

    void ShowLinePresenter()
    {
        linePresenterObject.SetActive(true);
    }

}
