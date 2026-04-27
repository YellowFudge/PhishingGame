using UnityEngine;
using UnityEngine.Events;

public static class CutsceneEventManager
{
    public static bool inCutscene;//not event so probably should move? ALSO NOT SURE IF THIS IS USEFUL ATM
    public static UnityAction<PersonsEnum, MoodEnum> ChangePersonEvent;
    public static UnityAction<MoodEnum> SetMoodEvent;
    public static UnityAction TriggerTalkingEvent;
    public static UnityAction TriggerExitingEvent;
    public static UnityAction TriggerEnteringEvent;
    /// <summary>
    /// Will be called when the end of an IT cutscene has been reached
    /// </summary>
    public static UnityAction EndOfITDialougeEvent;
    /// <summary>
    /// Will be called when the end of an evaluation/response cutscene has been reached
    /// </summary>
    public static UnityAction EndOfResponseDialougeEvent;
}
