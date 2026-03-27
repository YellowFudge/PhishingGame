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
    public static UnityAction EndOfITDialougeEvent;
    public static UnityAction EndOfResponseDialougeEvent;
}
