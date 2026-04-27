using UnityEngine;
using UnityEngine.Events;

public static class MailEventManager
{
    //public static UnityAction OpenScrollChuteEvent;
    //public static UnityAction CloseScrollChuteEvent;
    public static UnityAction ScrollStampedEvent;
    public static UnityAction ScrollOpenedEvent;
    public static UnityAction StampPickedUpEvent;
    public static UnityAction ScrollSentEvent;
    public static UnityAction<GameObject> ScrollLeaveChuteEvent;
    public static UnityAction<GameObject> TryPlaceScrollInChuteEvent;
    /// <summary>
    /// Scroll placed in chute.
    /// </summary>
    public static UnityAction ScrollPlacedInChuteEvent;//confirmation event
    /// <summary>
    /// Scroll couldn't be placed in chute. bool hasBeenOpened. True = has been opened. False = has not been opened.
    /// </summary>
    public static UnityAction<bool> ScrollNotPlacedInChuteEvent;//confirmation event
}
