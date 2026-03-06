using UnityEngine;
using UnityEngine.InputSystem;

public class CustomCursor : MonoBehaviour
{
    [SerializeField, Tooltip("The texture that is used when not clicking")] 
    Texture2D regularCursor;
    [SerializeField, Tooltip("The texture that is used when clicking")] 
    Texture2D clickedCursor;
    bool _clicking = false;

    private void OnEnable()
    {
        InputEventManager.ClickedPointerChangeEvent += OnCangeClickState;
    }

    private void OnDisable()
    {
        InputEventManager.ClickedPointerChangeEvent -= OnCangeClickState;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeCursor(regularCursor);
    }

    void OnCangeClickState()
    {
        if (_clicking)
        {
            ChangeCursor(regularCursor);
            _clicking = false;
            return;
        }

        ChangeCursor(clickedCursor);
        _clicking = true;
    }

    void ChangeCursor(Texture2D newCursor)
    {
        Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.Auto);
    }
}
