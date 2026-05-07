using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreTextInformation : MonoBehaviour
{
    TMP_Text _textAsset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string test = "Error\r\nIf an email has strange spelling, bad grammar, or unusual formatting, treat it as a warning sign. Errors can also include strange symbols or broken text (for example: Hello, {Boss}).\r\nWhat to do: Do not click the link, do not open attachments, and do not reply. Check the message through the company’s real website, app, or phone number. If you are at work, report it to your IT or security contact";
        _textAsset = GetComponent<TMP_Text>();
        _textAsset.text = test;
    }
}
