using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    void OnOneButton(InputValue value)
    {
        InputEventManager.OneButtonPressedEvent?.Invoke();
    }
    void OnTwoButton(InputValue value)
    {
        InputEventManager.TwoButtonPressedEvent?.Invoke();
    }
    void OnThreeButton(InputValue value)
    {
        InputEventManager.ThreeButtonPressedEvent?.Invoke();
    }
    void OnFourButton(InputValue value)
    {
        InputEventManager.FourButtonPressedEvent?.Invoke();
    }
    void OnSpaceButton(InputValue value)
    {
        InputEventManager.SpaceButtonPressedEvent?.Invoke();
    }
    void OnCancel(InputValue value) //esc and similar
    {
        InputEventManager.CancelButtonPressedEvent?.Invoke();
    }
}
