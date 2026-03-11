using UnityEngine;

public class MailCueTypes : MonoBehaviour
{
    [SerializeField] bool isSpamMail;
    [SerializeField, Tooltip("The cue types of the mail")] CueTypes[] cueTypeArray;

    public bool IsSpamMail { get {  return isSpamMail; } }
    public CueTypes[] CueTypeArray { get { return cueTypeArray; } }


    private void OnEnable()
    {
        if(isSpamMail && cueTypeArray.Length.Equals(0))
        {
            Debug.LogError($"The mail {this.gameObject.name} is marked as spam but has no cue types. Assign cue types.");
        }
        if (!isSpamMail && cueTypeArray.Length > 0)
        {
            Debug.LogWarning($"The mail {this.gameObject.name} is not marked as spam but has cue types. Set isSpamMail to true if this is not intentional.");
        }
    }

    /// <summary>
    /// Compares the internal cue types of a spam mail to the cue types in cueTypesToCompare
    /// </summary>
    /// <param name="cueTypesToCompare">the array of cuetypes to compare to the internal</param>
    /// <param name="numOfMatches">number of matches found (if is spam)</param>
    /// <returns>true if at least one match was found, false if no matches were found or isn't spam</returns>
    public bool CompareCueTypes(CueTypes[] cueTypesToCompare, out int numOfMatches)
    {
        numOfMatches = 0;

        if (!isSpamMail)
        {
            return false;
        }

        foreach(CueTypes compareType in cueTypesToCompare)
        {
            foreach(CueTypes internalType in cueTypeArray)
            {
                if(internalType == compareType)
                {
                    numOfMatches++;
                    continue;
                }
            }
        }

        if (numOfMatches.Equals(0))
        {
            return false;
        }
        return true;
    }

}

public enum CueTypes
{
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
}
