using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomScrollRect : ScrollRect
{
    private int _activePointerId = -1;

    public override void OnScroll(PointerEventData data)
    {
        base.OnScroll(data);
        velocity = Vector2.zero;
    }


    //-------------- Drag input tweaking -----------------
    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        //setting latest touch as current decider of drag
        _activePointerId = eventData.pointerId;
        base.OnInitializePotentialDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        //unsetting decider of drag if is current. Real pointerIDs will never be negative
        if (eventData.pointerId == _activePointerId)
        {
            base.OnEndDrag(eventData);
            _activePointerId = -1;
        }
            
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        /*//Only dragging if correct dragger
        if(eventData.pointerId == _activePointerId)
        {
            base.OnBeginDrag(eventData);
        }*/
        
    }

    public override void OnDrag(PointerEventData eventData)
    {
        /*//Only dragging if correct dragger
        if (eventData.pointerId == _activePointerId)
        {
            base.OnDrag(eventData);
        }*/
        
    }


}
