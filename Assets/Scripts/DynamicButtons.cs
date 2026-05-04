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
    
    [SerializeField] DailyCues dailyCues;
    [SerializeField] LevelManager levelManager;
    
    // Key = CueType (Name of button), Value = int (If toggled)
    Dictionary<CueTypes, int> enumDict = new Dictionary<CueTypes, int>();           // Stores QueTypes and toggled(0/1)
    private List<GameObject> buttons = new List<GameObject>();                   // Stores UI Toggles and state
    
    private int day;
    private int mailNo;
    private GameObject _currentMailObject;
    
    void Start()
    {
        enumDict.Clear();
        buttons.Clear();
        mailNo = levelManager.CurrentDay - 1;
        //Debug.Log(mailNo);
        ConvertEnumToDict();
        PrintButtons();
        //Debug.Log("Buttons Generated");
        //UpdateCueList();
    }

    public void UpdateCueList()
    {
        enumDict.Clear();
        mailNo = levelManager.CurrentDay - 1;
        //Debug.Log(mailNo);
        //CueTypes cue = dailyCues.dailyCuesArray[0].cues[2];
        //Debug.Log("Test: " + cue);
        ConvertEnumToDict();
        PrintButtons();
    }
    public void PrintButtons()
    {
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
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
            //Debug.Log(newButton.name);
        }
    }

    // Convert enum items from CueTypes in MailCueTypes.cs to dict for checklist and score.
    public Dictionary<CueTypes, int> ConvertEnumToDict()
    {
        // enumDict.Clear();
        
        /*for (int i = 0; i < dailyCues.dailyCuesArray.Length; i++)
        {
            foreach (CueTypes mail in dailyCues.dailyCuesArray[i].cues)
            {
                enumDict.Add(mail, 0);
            }
        }*/
        for (int i = 0; i <= mailNo; i++)
        {
            foreach (CueTypes mail in dailyCues.dailyCuesArray[i].cues)
            {
                enumDict.Add(mail, 0);
                //Debug.Log(enumDict[mail]);
            }
        }
        /*foreach (CueTypes mail in dailyCues.dailyCuesArray[mailNo].cues)
        {
            enumDict.Add(mail, 0);
            Debug.Log(enumDict[mail]);
        }*/
        
        //Debug.Log(enumDict.Count + " items converted to enumDict");
        return enumDict;
    }

    public void ClearCueList()
    {
        enumDict.Clear();
    }

    // Yerp you guessed it. This prints out the active state values in dictEnum. 
    public void PrintActiveStates()
    {
        Dictionary<CueTypes, int> dict = GetEnumDict();
        foreach (KeyValuePair<CueTypes, int> item in dict)
        {
            //Debug.unityLogger.Log("ToggleQueButtons " + item.Key + ", " + item.Value);
        }
    }
    public void ToggleCounter(CueTypes cueType, int toggled)
    {
        //Debug.Log("------------------------------------------------------------------------------");
        enumDict[cueType] = toggled;
        //Debug.Log($"{cueType} toggled state updated to: {toggled}");
        
        //Debug.Log(qb.cueType + " updated to: " + state);
        //Debug.Log(GetResult());
        PrintActiveStates();
    }

    public List<int> GetResult()
    {
            List<int> toggleStates = new List<int>();
            //toggleStates.Clear();
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

    public static string FormatButtonName(string input)
    {
        return Regex.Replace(
            Regex.Replace(input, "^btn", ""),
            "(?<=[a-z])([A-Z])|(?<=[A-Z])([A-Z])(?=[a-z])",
            " $0"
        );
    }

    public void ResetToggles()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<QueButton>().ResetState();
            Debug.Log("+++ RESETTING BUTTON +++");
        }
    }
    private void OnEnable()
    {
        CutsceneManager.EndOfEndDayTriggeredEvent += OnEndDayTriggered;
    }

    private void OnDisable()
    {
        CutsceneManager.EndOfEndDayTriggeredEvent -= OnEndDayTriggered;
    }

    void OnEndDayTriggered()
    {
        UpdateCueList();
    }
    
    private List<CueTypes> convertEnumToList() 
    {
        List<CueTypes> playerCueArray = new List<CueTypes>();
        foreach (KeyValuePair<CueTypes, int> item in GetEnumDict())
        {
            if (item.Value.Equals(1))
            {
                playerCueArray.Add(item.Key);
            }
        }

        return playerCueArray; 
    }
}
