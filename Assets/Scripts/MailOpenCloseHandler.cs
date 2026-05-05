using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MailOpenCloseHandler : OpenCloseHandler, IPointerDownHandler, IPointerUpHandler
{
    bool _isOutOfChute;
    bool _isOverChute; //ONLY USED FOR PARENTING
    bool _hasBeenOpened;
    [HideInInspector] public GraphicRaycaster graphicRaycaster;//on canvas

    //checks if it is over mailchute while no longer dragging, send ask to it if can be deposited there (yes if stamped) close if yes
    //MAYBE NEED TO CHECK ON IDropHandler AND IPointerUpHandler ALSO IF UNITY IS PICKY ON DRAG
    private void OnEnable()
    {
        MailEventManager.ScrollNotPlacedInChuteEvent += OnScrollNotPlaced;
        MailEventManager.ScrollPlacedInChuteEvent += OnScrollPlacedInChute;
        MailEventManager.StampPickedUpEvent += CloseObject;
    }

    private void OnDisable()
    {
        MailEventManager.ScrollNotPlacedInChuteEvent -= OnScrollNotPlaced;
        MailEventManager.ScrollPlacedInChuteEvent -= OnScrollPlacedInChute;
        MailEventManager.StampPickedUpEvent -= CloseObject;
    }

    private void Awake()
    {
        _isOutOfChute = false;
        _isOverChute = true;
        _hasBeenOpened = false;
    }

    void OnScrollNotPlaced(bool hasBeenOpened)
    {
        if (!hasBeenOpened)
        {
            //shake letter to indicate that player should open it

        }
    }

    void OnScrollPlacedInChute()
    {
        //close scroll
        base.CloseObject();
    }

    public void OnPointerDown(PointerEventData eventData)//When dragging/open closing first time/while in chute -> move out of chute (call chute that is happening)
    {
        //check if it is still over chute so can parent correctly??
        if (!_isOutOfChute) //do so it is always? Need to always be above chute when over chute(same for when moving away from over chute)
        {
            _isOutOfChute = true;
            MailEventManager.ScrollLeaveChuteEvent?.Invoke(gameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Check if object is over chute
        Image currentGOImage;
        if (_opened)
        {
            currentGOImage = openedObject.GetComponent<Image>();
        }
        else
        {
            currentGOImage = closedObject.GetComponent<Image>();
        }
        currentGOImage.raycastTarget = false;
        //check if chute is beneath

        //raycasting to object beneath and seeing it it is the chute
        List<RaycastResult> results = new List<RaycastResult>();//LATER move to memberVs to same memory!!
        graphicRaycaster.Raycast(eventData, results);

        if (results.Count > 0)
        {
            /*foreach (RaycastResult go in results)
            {
                Debug.Log(go.gameObject.name);
            }*/
            if (results[0].gameObject.tag.Equals("MailChute"))
            {
                //if hasn't just released the mail from chute -> try to place it in chute
                MailEventManager.TryPlaceScrollInChuteEvent?.Invoke(gameObject);
            }
        }

        currentGOImage.raycastTarget = true;
        //if hasn't just released the mail from chute -> try to place it in chute<-not needed??
    }

    protected override void ChangeObjectState(bool doOpenObject)
    {
        base.ChangeObjectState(doOpenObject);

        if (doOpenObject && !_hasBeenOpened)
        {
            _hasBeenOpened = true;
            MailEventManager.ScrollOpenedEvent?.Invoke();
        }
    }

    IEnumerator CheckIsOverScrollChute()
    {
        while (_dragging)
        {
            if (!_isOverChute)
            {
                //check for if is over chute -> make it parent
            }
                yield return null;
        }
    }

}
