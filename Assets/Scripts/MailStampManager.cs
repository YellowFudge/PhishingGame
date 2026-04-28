using Unity.VisualScripting;
using UnityEngine;

public class MailStampManager : MonoBehaviour
{
    [SerializeField] MailStamp phishStamp;
    [SerializeField] MailStamp hamStamp;
    [SerializeField] GameObject stampCover;
    Animator _animator;
    bool _hasStamped;
    bool _stampedAsPhishing;

    public bool HasStamped { get { return _hasStamped; }}
    public bool StampedAsPhishing { get { return _stampedAsPhishing; }}

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        MailEventManager.ScrollOpenedEvent += OnScrollOpened;
        MailEventManager.ScrollSentEvent += ResetStamps;//make subscribe to when begins closing (sending)
                                                        //so cannot extract stamps while it is closing to stamp next
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
            _animator.SetTrigger("Open");
            //stampCover.SetActive(false);
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
        //stampCover.SetActive(true);
        _animator.SetTrigger("Close");
        _hasStamped = false;
    }

    
}
