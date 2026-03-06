
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    Vector3 _pointerOffsetToMiddle;
    RectTransform _rectTrans;

    private void OnEnable()
    {
        _pointerOffsetToMiddle = Vector3.zero;
        _rectTrans = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) //TODO: MAKE IT AT FRONT OF OTHER THINGS WHEN SELECTED/DRAGGED
    {
        //calculate offset between mousepos and middle of object
        _pointerOffsetToMiddle = transform.position - InputEventManager.PointPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //check that new center is inside frustum, setting it to inside coordinates if not
        Rect screenBounds = new Rect(0f,0f,Screen.width, Screen.height);
        Vector3 newCenterpos = InputEventManager.PointPos + _pointerOffsetToMiddle;

        Vector3 newPos = transform.position;

        if (newCenterpos.x > screenBounds.xMin && newCenterpos.x < screenBounds.xMax)
        {
            newPos.x = (InputEventManager.PointPos + _pointerOffsetToMiddle).x;
        }
        else
        {
            if (newCenterpos.x < screenBounds.xMin)
            {
                newPos.x = screenBounds.xMin;
            }
            else if (newCenterpos.x > screenBounds.xMax)
            {
                newPos.x = screenBounds.xMax;
            }
        }
        if (newCenterpos.y > screenBounds.yMin && newCenterpos.y < screenBounds.yMax)
        {
            newPos.y = (InputEventManager.PointPos + _pointerOffsetToMiddle).y;
        }
        else
        {
            if (newCenterpos.y < screenBounds.yMin)
            {
                newPos.y = screenBounds.yMin;
            }
            else if (newCenterpos.y > screenBounds.yMax)
            {
                newPos.y = screenBounds.yMax;
            }
        }

        transform.position = newPos;
        _rectTrans.SetAsLastSibling(); //making in front of everything under the same parent

    }
}
