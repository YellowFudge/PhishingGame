using UnityEngine;
using UnityEngine.Events;

public static class InputEventManager
{
    public static UnityAction OneButtonPressedEvent;
    public static UnityAction TwoButtonPressedEvent;
    public static UnityAction ThreeButtonPressedEvent;
    public static UnityAction FourButtonPressedEvent;
    public static UnityAction SpaceButtonPressedEvent;
    public static UnityAction CancelButtonPressedEvent;
    public static Vector3 pointPos;//z will always be 0
}
