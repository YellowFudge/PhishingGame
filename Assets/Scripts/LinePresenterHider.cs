using UnityEngine;

public class LinePresenterHider : MonoBehaviour
{
    [SerializeField] GameObject linePresenterObject;

    private void OnEnable()
    {
        CutsceneManager.EndOfFadeAwayBlackTriggeredEvent += HideLinePresenter;
        CutsceneManager.EndOfEndDayTriggeredEvent += ShowLinePresenter;
    }

    private void OnDisable()
    {
        CutsceneManager.EndOfFadeAwayBlackTriggeredEvent -= HideLinePresenter;
        CutsceneManager.EndOfEndDayTriggeredEvent -= ShowLinePresenter;
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
