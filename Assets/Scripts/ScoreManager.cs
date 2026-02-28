using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    Dictionary<string, int> queDict = new Dictionary<string, int>();
    [SerializeField] private Toggle[] checklistOptions;
    private List<Toggle> toggles = new List<Toggle>();
    
    public List<Toggle> OnCheckboxPressed()
    {
        toggles.Clear();
        foreach (Toggle toggle in checklistOptions)
        {
            if (toggle.isOn)
            {
                toggles.Add(toggle);
                Debug.Log("Toggled: " + toggle.name);
            }
        }
        Debug.Log("Toggles total: " + toggles.Count);
        foreach (var check in toggles)
        {
            Debug.Log(check.name);
        }
        return toggles;
    }

    public void OnFinalSendPressed()
    {
        List<Toggle> score = OnCheckboxPressed();
        Debug.Log("Wizard sent " + score.Count);
        
    }

    public void OnFinalRejectPressed()
    {
        List<Toggle> score = OnCheckboxPressed();
        Debug.Log("Wizard Rejected " + score.Count);
    } 
}
