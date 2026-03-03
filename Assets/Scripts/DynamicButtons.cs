using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class DynamicButtons : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonField;
    // Key = CueType (Name of button), Value = int (If toggled)
    Dictionary<CueTypes, int> enumDict = new Dictionary<CueTypes, int>();           // Stores QueTypes and toggled(0/1)
    private List<GameObject> buttons = new List<GameObject>();                      // Stores UI Toggles and state
    
    void Start()
    {
        ConvertEnumToDict();
        PrintButtons();
        Debug.Log("Buttons Generated");
    }
    public void PrintButtons()
    {
        buttons.Clear();
        foreach (KeyValuePair<CueTypes, int> item in enumDict)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonField.transform);
            newButton.name = "btn" + item.Key;
            QueButton qb = newButton.GetComponent<QueButton>();
            qb.buttonText.text = FormatButtonName(item.Key.ToString());
            qb.toggled = item.Value;
            qb.cueType = item.Key;
            
            buttons.Add(newButton);
        }
    }

    // Convert enum items from CueTypes in MailCueTypes.cs to dict for checklist and score.
    public Dictionary<CueTypes, int> ConvertEnumToDict()
    {
        enumDict.Clear();
        foreach (CueTypes type in (CueTypes[])System.Enum.GetValues(typeof(CueTypes)))
        {
            enumDict.Add(type, 0);
        }
        Debug.Log(enumDict.Count + " items converted to enumDict");
        return enumDict;
    }

    // Yerp you guessed it. This prints out the active state values in dictEnum. 
    public void PrintActiveStates()
    {
        Dictionary<CueTypes, int> dict = GetEnumDict();
        foreach (KeyValuePair<CueTypes, int> item in dict)
        {
            Debug.unityLogger.Log("ToggleQueButtons " + item.Key + ", " + item.Value);
        }
    }
    public void ToggleCounter(CueTypes cueType, int toggled)
    {
        Debug.Log("------------------------------------------------------------------------------");
        enumDict[cueType] = toggled;
        Debug.Log($"{cueType} toggled state updated to: {toggled}");
        
        //PrintActiveStates();
        //Debug.Log(qb.cueType + " updated to: " + state);
        //Debug.Log(GetResult());
        
        PrintActiveStates();
    }

    public List<int> GetResult()
    {
            List<int> toggleStates = new List<int>();
            toggleStates.Clear();
            foreach (KeyValuePair<CueTypes, int> item in enumDict)
            {
                toggleStates.Add(item.Value);
            }
            return toggleStates;
    }

    public Dictionary<CueTypes, int> GetEnumDict()
    {
        return enumDict;
    }

    /*public string ToggleTextGenerator(string enumTypeName)
    {
        string toggleText;
        
        return toggleText;
    }*/
    public static string FormatButtonName(string input)
    {
        return Regex.Replace(
            Regex.Replace(input, "^btn", ""),
            "(?<=[a-z])([A-Z])|(?<=[A-Z])([A-Z])(?=[a-z])",
            " $0"
        );
    }
}
