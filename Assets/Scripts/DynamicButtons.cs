using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicButtons : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonField;
    private List<string> enumArr = new List<string>();
    private List<GameObject> buttons = new List<GameObject>();
    Dictionary<CueTypes, bool> enumDict = new Dictionary<CueTypes, bool>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConvertEnumToList();
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

        foreach (KeyValuePair<CueTypes, bool> item in enumDict)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonField.transform);
            newButton.name = "btn_" + item.Key;
            QueButton qb = newButton.GetComponent<QueButton>();
            qb.buttonText.text = item.Key.ToString();
            buttons.Add(newButton);
        }
        /*foreach (var item in enumArr)
        {
            Debug.Log(item);
        }*/
    }

    // Convert enum items from CueTypes in MailCueTypes.cs to list for checklist and score.
    public List<string> ConvertEnumToList()
    {
        enumArr.Clear();
        foreach (CueTypes type in (CueTypes[])System.Enum.GetValues(typeof(CueTypes)))
        {
            enumArr.Add(type.ToString());
        }
        Debug.Log(enumArr.Count + " items converted to enumArr");
        return enumArr;
    }
    
    public Dictionary<CueTypes, bool> ConvertEnumToDict()
    {
        enumDict.Clear();
        foreach (CueTypes type in (CueTypes[])System.Enum.GetValues(typeof(CueTypes)))
        {
            enumDict.Add(type, false);
        }
        Debug.Log(enumArr.Count + " items converted to enumDict");
        return enumDict;
    }

    public void ToggleQueButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
    }
}
