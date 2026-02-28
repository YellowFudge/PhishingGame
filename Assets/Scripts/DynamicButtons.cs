using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicButtons : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonField;
    
    private List<GameObject> buttons = new List<GameObject>();                      
    
    // Key = CueType (Name of button), Value = int (If toggled)
    Dictionary<CueTypes, int> enumDict = new Dictionary<CueTypes, int>();           // Stores QueTypes and toggled(0/1)
    Dictionary<GameObject, int> CueButtons = new Dictionary<GameObject, int>();     // Stores the GameObject buttons and toggled (0/1)
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConvertEnumToDict();
        PrintButtons();
        Debug.Log("Buttons Generated");
    }
    public void PrintButtons()
    {
        /*for (int i = 0; i < enumArr.Count; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonField.transform);
            newButton.name = "btn" + enumArr[i];
            newButton.GetComponent<QueButton>().buttonText.text = enumArr[i].ToString();
            buttons.Add(newButton);
        }*/
        CueButtons.Clear();
        foreach (KeyValuePair<CueTypes, int> item in enumDict)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonField.transform);
            newButton.name = "btn" + item.Key;
            QueButton qb = newButton.GetComponent<QueButton>();
            qb.buttonText.text = item.Key.ToString();
            qb.toggled = item.Value;
            buttons.Add(newButton);
        }
        /*foreach (var item in enumArr)
        {
            Debug.Log(item);
        }*/
        UpdateDictionaryFromUI();
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

    public void ToggleQueButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
    }
    public void UpdateDictionaryFromUI()
    {
        foreach (GameObject obj in buttons)
        {
            Debug.Log(obj.GetComponent<QueButton>().buttonText.text + " Exists!");
            /*QueButton qb = obj.GetComponent<QueButton>();
            enumDict[qb.toggled] = qb.toggled.isOn;*/
        }
        Debug.Log("Buttons do exist actually!!!!!");
    }
}
