using System.Collections.Generic;
using UnityEngine;

public class DynamicButtons : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonField;
    private List<string> enumArr = new List<string>();
    private List<GameObject> buttons = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConvertEnumToArr();
        PrintButtons();
        Debug.Log("Buttons Generated");
    }
    public void PrintButtons()
    {
        for (int i = 0; i < enumArr.Count; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonField.transform);
            newButton.name = "btn" + enumArr[i];
            newButton.GetComponent<QueButton>().buttonText.text = enumArr[i].ToString();
            buttons.Add(newButton);
        }
        //foreach (var item in enumArr)
        //{
        //Debug.Log(item);
        //}
    }

    // Convert enum items from CueTypes in MailCueTypes.cs to list for checklist and score.
    public List<string> ConvertEnumToArr()
    {
        enumArr.Clear();
        foreach (CueTypes type in (CueTypes[])System.Enum.GetValues(typeof(CueTypes)))
        {
            enumArr.Add(type.ToString());
        }
        Debug.Log(enumArr.Count + " items converted to enumArr");
        return enumArr;
    }

    public void ToggleQueButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
    }
}