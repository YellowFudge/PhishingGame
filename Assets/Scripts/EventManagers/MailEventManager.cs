using UnityEngine;
using UnityEngine.Events;

public static class MailEventManager
{
    /// <summary>
    /// Triggered each time the scroll has been successfully stamped by either of the MailStamps
    /// </summary>
    public static UnityAction ScrollStampedEvent;
    /// <summary>
    /// Triggered the first time each scoll is opened
    /// </summary>
    public static UnityAction ScrollOpenedEvent;
    /// <summary>
    /// Triggered when either of the stamps have been picked up by player
    /// </summary>
    public static UnityAction StampPickedUpEvent;
    /// <summary>
    /// Triggered when closing animation of ScrollChute starts playing while sending away stamped scroll
    /// </summary>
    public static UnityAction SendingScrollEvent;
    /// <summary>
    /// Triggered when the closing animation of the ScollChute is done playing while sending away stamped scroll (scroll is out of frame)
    /// </summary>
    public static UnityAction ScrollSentEvent;
    /// <summary>
    /// Triggered every time scroll has been moved out of ScrollChute. Gameobject is the scroll
    /// </summary>
    public static UnityAction<GameObject> ScrollLeaveChuteEvent;
    /// <summary>
    /// Triggered if scroll is dropped by player while over ScrollChute
    /// </summary>
    public static UnityAction<GameObject> TryPlaceScrollInChuteEvent;
    /// <summary>
    /// Confirmation event. Scroll could be placed in ScrollChute.
    /// </summary>
    public static UnityAction ScrollPlacedInChuteEvent;//confirmation event
    /// <summary>
    /// Confirmation event (unsuccessful). Scroll couldn't be placed in ScrollChute. bool hasBeenOpened. True = scroll has been opened. False = scroll has not been opened.
    /// </summary>
    public static UnityAction<bool> ScrollNotPlacedInChuteEvent;//confirmation event
}
