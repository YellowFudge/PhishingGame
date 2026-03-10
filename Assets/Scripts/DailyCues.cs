using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyCues", menuName = "Scriptable Objects/DailyCues")]
public class DailyCues : ScriptableObject
{
    public CueArray[] dailyCuesArray;
}

[Serializable] 
public class CueArray
{
    public CueTypes[] cues;
}
