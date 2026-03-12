using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QueButton : MonoBehaviour
{
    public CueTypes cueType;
    public TMP_Text buttonText;
    public int toggled; // 0 or 1
    private DynamicButtons dynamicButtons;
    public Toggle toggle;

    void Start()
    {
        dynamicButtons = FindObjectOfType<DynamicButtons>();
        toggle.SetIsOnWithoutNotify(toggled == 1);
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        toggled = isOn ? 1 : 0;
        dynamicButtons.ToggleCounter(cueType, toggled);
    }

    public void ResetValue()
    {
        toggle.isOn = false;
    }
}