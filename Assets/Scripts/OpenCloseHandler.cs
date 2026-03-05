using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class OpenCloseHandler : MonoBehaviour, IPointerClickHandler, /*ISubmitHandler,*/ IBeginDragHandler, IEndDragHandler
{
    [SerializeField] GameObject openedObject;
    [SerializeField] GameObject closedObject;
    bool _opened;
    bool _dragging;
    RectTransform _rectTrans;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openedObject.SetActive(true);
        closedObject.SetActive(false);
        _opened = true;
        _dragging = false;
        _rectTrans = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //return if dragging or clicking on object marked as to be ignored by this script
        if (_dragging || eventData.rawPointerPress.TryGetComponent(out IgnoreOpenClose ignorer)) return;


        //checking position under parent
        if (!(_rectTrans.parent.childCount - 1).Equals(_rectTrans.GetSiblingIndex()))
        {
            _rectTrans.SetAsLastSibling(); //making in front of everything under the same parent
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
}
