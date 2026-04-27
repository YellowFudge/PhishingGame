using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class OpenCloseHandler : MonoBehaviour, IPointerClickHandler, /*ISubmitHandler,*/ IBeginDragHandler, IEndDragHandler
{
    [SerializeField] protected GameObject openedObject;
    [SerializeField] protected GameObject closedObject;
    [SerializeField] protected bool startOpen;
    protected bool _opened;
    protected bool _dragging;
    protected RectTransform _rectTrans;
    protected bool _playerHasOpened;

    /// <summary>
    /// true if the object is currently open. false if not.
    /// </summary>
    public bool IsOpen { get { return _opened; } }
    /// <summary>
    /// true player has ever opened this object. false if not.
    /// </summary>
    public bool PlayerHasOpened { get { return _playerHasOpened; } }

    protected void Start()
    {
        if (startOpen)
        {
            ChangeObjectState(true);
        }
        else
        {
            ChangeObjectState(false);
        }

        _playerHasOpened = false;
        _dragging = false;
        _rectTrans = GetComponent<RectTransform>();
    }

    public void CloseObject()
    {
        ChangeObjectState(false);
    }

    protected virtual void ChangeObjectState(bool doOpenObject)
    {
        if (doOpenObject)
        {
            openedObject.SetActive(true);
            closedObject.SetActive(false);
            _opened = true;
        }
        else
        {
            openedObject.SetActive(false);
            closedObject.SetActive(true);
            _opened = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //return if dragging or clicking on object marked as to be ignored by this script
        if (_dragging || eventData.rawPointerPress.TryGetComponent(out IgnoreOpenClose ignorer)) return;


        //checking position under parent
        if (!(_rectTrans.parent.childCount - 1).Equals(_rectTrans.GetSiblingIndex()))
        {
            //check if is overlapped by other UI object
            Vector3[] cornersA = new Vector3[4];

            RectTransform rectA = null;

            //find your own active child
            for (int i = 0; i < _rectTrans.childCount; i++)
            {
                if (_rectTrans.parent.GetChild(i).gameObject.activeSelf)
                    {
                        rectA = _rectTrans.parent.GetChild(i).GetComponent<RectTransform>();
                        break;
                    }
                
            }
            if (rectA == null)
            {
                rectA = _rectTrans; //return instead?
            }

            for(int i = 0; i < _rectTrans.parent.childCount; i++)
            {
                if (i.Equals(rectA.GetSiblingIndex()) || !_rectTrans.parent.GetChild(i).gameObject.activeSelf)
                {
                    continue;
                }

                RectTransform rectB = null;

                if (_rectTrans.parent.GetChild(i).childCount > 1)
                {
                    if(_rectTrans.parent.GetChild(i).TryGetComponent(out CutsceneManager cutManager)){ //Make this prettier later on
                        continue;
                    }

                    for( int j = 0; j < _rectTrans.parent.GetChild(i).childCount; j++)
                    {
                        //Debug.Log(_rectTrans.parent.GetChild(i).GetChild(j).gameObject.name);
                        if (_rectTrans.parent.GetChild(i).GetChild(j).gameObject.activeSelf)
                        {
                            rectB = _rectTrans.parent.GetChild(i).GetChild(j).GetComponent<RectTransform>();
                            break;
                        }
                    }
                    if(rectB == null)
                    {
                        return;
                    }

                    //Debug.Log(rectB);
                    
                }
                else
                {
                    rectB = _rectTrans.parent.GetChild(i).GetComponent<RectTransform>();
                }

                     

                if (CheckOverlap(cornersA, rectA, rectB))//needs to be moved to the front before you can interact with it
                {
                    _rectTrans.SetAsLastSibling(); //making in front of everything under the same parent
                    return;
                }
            }
            _rectTrans.SetAsLastSibling(); //already "at front" from player's prespective, so they want to interact with it as well == no return

        }
        

        if (_opened)
        {
            ChangeObjectState(false);
            return;
        }
        _playerHasOpened = true;
        ChangeObjectState(true);
    }

    /*public void OnSubmit(BaseEventData eventData) //only for when using gamepad-/button navigation(therefore ignored for now) 
    {
        if (_dragging) return;

        if (eventData.selectedObject.TryGetComponent(out IIgnoreOpenClose ignorer))
        {
            return;
        }

        if (_opened)
        {
            openedObject.SetActive(false);
            closedObject.SetActive(true);
            _opened = false;
            return;
        }
        openedObject.SetActive(true);
        closedObject.SetActive(false);
        _opened = true;
    }*/

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragging = false;
    }
    protected bool CheckOverlap(Vector3[] ownCorners, RectTransform ownRect, RectTransform otherRect)
    {
        //check if is overlapped by other UI object
        Vector3[] cornersB = new Vector3[4];

        ownRect.GetWorldCorners(ownCorners);
        otherRect.GetWorldCorners(cornersB);

        Rect rect1 = new Rect(ownCorners[0], ownCorners[2] - ownCorners[0]);
        Rect rect2 = new Rect(cornersB[0], cornersB[2] - cornersB[0]);

        if (rect1.Overlaps(rect2))//needs to be moved to the front before you can interact with it
        { 
            return true;
        }
        else
        {
            return false;
        }
    }
}
