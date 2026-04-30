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
    bool _interactedWithStamps;

    public bool HasStamped { get { return _hasStamped; }}
    public bool StampedAsPhishing { get { return _stampedAsPhishing; }}

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _interactedWithStamps = false;
    }

    private void OnEnable()
    {
        MailEventManager.ScrollOpenedEvent += OnScrollOpened;
        MailEventManager.SendingScrollEvent += ResetStamps;//make subscribe to when begins closing (sending)
                                                        //so cannot extract stamps while it is closing to stamp next
    }

    private void OnDisable()
    {
        MailEventManager.ScrollOpenedEvent -= OnScrollOpened;
        MailEventManager.SendingScrollEvent -= ResetStamps;
    }

    void OnScrollOpened()
    {
        //Remove cover (will always be closed when called)
        _animator.SetTrigger("Open");
    }

    public void StampPickedUp()
    {
        if (!_interactedWithStamps)
        {
            MailEventManager.StampPickedUpEvent?.Invoke();
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
        _animator.SetTrigger("Close");

        _hasStamped = false;
        _interactedWithStamps = false;
    }

    
}
