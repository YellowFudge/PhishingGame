using System;
using UnityEngine;

[CreateAssetMenu(menuName = "String Day Array")]
public class StringDayArrayScriptObj : ScriptableObject
{
    [SerializeField] StringArray[] dayArray;

    [Serializable]
    class StringArray //needed to be able to edit it in inspector
    {
        public string[] stringArray;
    }

    public int GetNumOfDays()
    {
        return dayArray.Length;
    }
    /// <summary>
    /// Tries to get an array of strings for the given dayIndex
    /// </summary>
    /// <param name="dayIndex">0-based number for requested day (first day == 0)</param>
    /// <param name="strings">the returned array of strings for the given day if exsists</param>
    /// <returns>true if array can be found. false if it cannot</returns>
    public bool GetDayStrings(int dayIndex, out string[] strings)
    {
        if (dayIndex > -1 && dayArray.Length > dayIndex)
        {
            strings = dayArray[dayIndex].stringArray;
            return true;
        }

        strings = null;
        return false;
    }
    /// <summary>
    /// Tries to get the string on the requested location in array
    /// </summary>
    /// <param name="dayIndex">0-based number for requested day array (first day == 0)</param>
    /// <param name="stringIndex">0-based number for requested string in day array (first string == 0)</param>
    /// <param name="requestedString">the returned string if location exsists</param>
    /// <returns>true if location in array can be found. false if it cannot</returns>
    public bool GetCurrentString(int dayIndex, int stringIndex, out string requestedString)
    {
        if (dayIndex > -1 && dayArray.Length > dayIndex && dayArray[dayIndex].stringArray.Length > stringIndex)
        {
            requestedString = dayArray[dayIndex].stringArray[stringIndex];
            return true;
        }
        requestedString = null;
        return false;
    }

}


