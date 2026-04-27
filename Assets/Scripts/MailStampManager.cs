using Unity.VisualScripting;
using UnityEngine;

public class MailStampManager : MonoBehaviour
{
    [SerializeField] MailStamp phishStamp;
    [SerializeField] MailStamp hamStamp;
    [SerializeField] GameObject stampCover;
    bool _hasStamped;
    bool _stampedAsPhishing;

    public bool HasStamped { get { return _hasStamped; }}
    public bool StampedAsPhishing { get { return _stampedAsPhishing; }}

    private void OnEnable()
    {
        MailEventManager.ScrollOpenedEvent += OnScrollOpened;
        MailEventManager.ScrollSentEvent += ResetStamps;
    }

    private void OnDisable()
    {
        MailEventManager.ScrollOpenedEvent -= OnScrollOpened;
        MailEventManager.ScrollSentEvent -= ResetStamps;
    }

    void OnScrollOpened()
    {
        //Remove cover (if not already opened)
        if (stampCover.activeSelf)
        {
            stampCover.SetActive(false);
        }
    }

    public void StampUsed(bool isPhising)
    {
        _hasStamped = true;
        _stampedAsPhishing = isPhising;
        MailEventManager.ScrollStampedEvent?.Invoke();
    }

    public void ResetStamps()
    {
        //cover with lid again
        stampCover.SetActive(true);
        _hasStamped = false;
    }
}
