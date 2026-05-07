using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    [SerializeField] ScoreScriptableObjectScript scoreScriptObj;
    TMP_Text _textAsset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreScriptObj.playerPercentRightQue_Error = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_Error / (scoreScriptObj.playerChoseCorrectQue_Error + scoreScriptObj.playerChoseWrongQue_Error) * 100f);
        scoreScriptObj.playerPercentRightQue_SenderDomain = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_SenderDomain / (scoreScriptObj.playerChoseCorrectQue_SenderDomain + scoreScriptObj.playerChoseWrongQue_SenderDomain) * 100f);
        scoreScriptObj.playerPercentRightQue_TooGoodToBeTrue = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_TooGoodToBeTrue / (scoreScriptObj.playerChoseCorrectQue_TooGoodToBeTrue + scoreScriptObj.playerChoseWrongQue_TooGoodToBeTrue) * 100f);
        scoreScriptObj.playerPercentRightQue_GenericGreeting = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_GenericGreeting / (scoreScriptObj.playerChoseCorrectQue_GenericGreeting + scoreScriptObj.playerChoseWrongQue_GenericGreeting) * 100f);
        scoreScriptObj.playerPercentRightQue_LogoImitiation = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_LogoImitiation / (scoreScriptObj.playerChoseCorrectQue_LogoImitiation + scoreScriptObj.playerChoseWrongQue_LogoImitiation) * 100f);
        scoreScriptObj.playerPercentRightQue_NoBranding = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_NoBranding / (scoreScriptObj.playerChoseCorrectQue_NoBranding + scoreScriptObj.playerChoseWrongQue_NoBranding) * 100f);
        scoreScriptObj.playerPercentRightQue_URLOrAttachment = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_URLOrAttachment / (scoreScriptObj.playerChoseCorrectQue_URLOrAttachment + scoreScriptObj.playerChoseWrongQue_URLOrAttachment) * 100f);
        scoreScriptObj.playerPercentRightQue_RequestInfo = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_RequestInfo / (scoreScriptObj.playerChoseCorrectQue_RequestInfo + scoreScriptObj.playerChoseWrongQue_RequestInfo) * 100f);
        scoreScriptObj.playerPercentRightQue_Urgency = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_Urgency / (scoreScriptObj.playerChoseCorrectQue_Urgency + scoreScriptObj.playerChoseWrongQue_Urgency) * 100f);
        scoreScriptObj.playerPercentRightQue_PosesAs = Mathf.Round((float)scoreScriptObj.playerChoseCorrectQue_PosesAs / (scoreScriptObj.playerChoseCorrectQue_PosesAs + scoreScriptObj.playerChoseWrongQue_PosesAs) * 100f);

        /*
        Error,
        SenderDomain,
        TooGoodToBeTrue,
        GenericGreeting,
        LogoImitiation,
        NoBranding,
        URLOrAttachment,
        RequestInfo,
        Urgency,
        PosesAs
        */

        _textAsset = GetComponent<TMP_Text>();
        _textAsset.text =
            $"Total number of correct scrolls: {scoreScriptObj.totalCorrectMail}/{scoreScriptObj.totalMail}" +
            $"Error: {scoreScriptObj.playerPercentRightQue_Error}%\n" +
            $"Sender Domain: {scoreScriptObj.playerPercentRightQue_SenderDomain}%\n" +
            $"Too Good To Be True: {scoreScriptObj.playerPercentRightQue_TooGoodToBeTrue}%\n" +
            $"Generic Greeting: {scoreScriptObj.playerPercentRightQue_GenericGreeting}%\n" +
            $"Logo Imitiation: {scoreScriptObj.playerPercentRightQue_LogoImitiation}%\n" +
            $"No Branding: {scoreScriptObj.playerPercentRightQue_NoBranding}%\n" +
            $"URL Or Attachment: {scoreScriptObj.playerPercentRightQue_URLOrAttachment}%\n" +
            $"Request Info: {scoreScriptObj.playerPercentRightQue_RequestInfo}%\n" +
            $"Urgency: {scoreScriptObj.playerPercentRightQue_Urgency}%\n" +
            $"Poses As: {scoreScriptObj.playerPercentRightQue_PosesAs}%\n";
    }
}