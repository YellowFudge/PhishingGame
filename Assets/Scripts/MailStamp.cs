using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MailStamp : MonoBehaviour, /*IDropHandler,*/ IBeginDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] bool isPhishingStamp;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform stampingPosition;
    [SerializeField] GameObject stampObject;
    [SerializeField] GraphicRaycaster graphicRaycaster;//on canvas
    [SerializeField] MailStampManager mailStampManager;
    Image _stampImage;

    private void Awake()
    {
        _stampImage = GetComponent<Image>();
        transform.position = startPosition.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        MailEventManager.StampPickedUpEvent?.Invoke();
        _stampImage.raycastTarget = false;
    }

    /*public void OnDrop(PointerEventData eventData) //on pointer up as well? now it will always drag though so this might be enough
    {
        //raycasting to object beneath and seeing it it is a mail
        _stampImage.raycastTarget = true;//needs to be last to ensure that this isn't hit by raycast
    }*/

    public void OnPointerUp(PointerEventData eventData)
    {
        //raycasting to object beneath and seeing it it is a mail
        List<RaycastResult> results = new List<RaycastResult>();//LATER move to memberVs to same memory!!
        eventData.position = stampingPosition.position;
        graphicRaycaster.Raycast(eventData, results);

        if (results.Count > 0)
        {
            foreach(RaycastResult go in results)
            {
                Debug.Log(go.gameObject.name);
            }
            if(results[0].gameObject.tag == "Mail")
            {
                //put stamp where mouse was
                Instantiate(stampObject, eventData.position, Quaternion.identity, results[0].gameObject.transform);
                mailStampManager.StampUsed(isPhishingStamp);
            }
        }
        transform.position = startPosition.position;
        
        _stampImage.raycastTarget = true;//needs to be last to ensure that this isn't hit by raycast
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("down");
    }
}
