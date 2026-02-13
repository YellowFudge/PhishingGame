using UnityEngine;

public class MailQueTypes : MonoBehaviour
{
    [SerializeField] bool isSpamMail;
    [SerializeField, Tooltip("The que types of the mail")] QueTypes[] queTypeArray;

    public bool IsSpamMail { get {  return isSpamMail; } }
    public QueTypes[] QueTypeArray { get { return queTypeArray; } }


    private void OnEnable()
    {
        if(isSpamMail && queTypeArray.Length.Equals(0))
        {
            Debug.LogError($"The mail {this.gameObject.name} is marked as spam but has no que types. Assign que types.");
        }
    }

    /// <summary>
    /// Compares the internal que types of a spam mail to the que types in queTypesToCompare
    /// </summary>
    /// <param name="queTypesToCompare">the array of quetypes to compare to the internal</param>
    /// <param name="numOfMatches">number of matches found (if is spam)</param>
    /// <returns>true if at least one match was found, false if no matches were found or isn't spam</returns>
    public bool CompareQueTypes(QueTypes[] queTypesToCompare, out int numOfMatches)
    {
        numOfMatches = 0;

        if (!isSpamMail)
        {
            return false;
        }

        foreach(QueTypes compareType in queTypesToCompare)
        {
            foreach(QueTypes internalType in queTypeArray)
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

public enum QueTypes
{
    Error,
    TechnicalIndicator,
    Visuals,
    Language,
    CommonTactics
}
