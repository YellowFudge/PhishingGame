using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class YarnspinnerManager : MonoBehaviour 
{
    [SerializeField] DialogueRunner dialougeRunner;

    /// <summary>
    /// bool == isEndOfResponse (false== end of IT dialouge)
    /// </summary>
    private static UnityAction<bool> EndOfDialougeEvent; //is needed to go around static (add delay from yarn to monobehaviour)

    private void OnEnable()
    {
        EndOfDialougeEvent += TriggerWait;
    }
    private void OnDisable()
    {
        EndOfDialougeEvent -= TriggerWait;
    }

    void TriggerWait(bool isEndOfResponse)
    {
        StartCoroutine(WaitForNodeEscape(isEndOfResponse));
    }

    IEnumerator WaitForNodeEscape(bool isEndOfResponse)
    {
        // Wait for yarn to jump out of node (ugly but only solution found so far)
        yield return new WaitForSeconds(0.1f);

        if (isEndOfResponse)
        {
            //send to the one dealing with IT dialouges
            CutsceneEventManager.EndOfResponseDialougeEvent?.Invoke();
        }
        else
        {
            //send to cutSceneManager
            CutsceneEventManager.EndOfITDialougeEvent?.Invoke();
        }  
    }
    public void StartDialouge(string dialougeID)//for triggering dialouge cutscenes
    {
        //check if ID exsist 
        if (!dialougeRunner.Dialogue.NodeExists(dialougeID))
        {
            Debug.LogError($"Requested dialouge node \"{dialougeID}\" does not exist. Use existing node names");
            return;
        }
        //TODO: if exsists -> check a dialouge is not already running

        //if not -> start dialouge
        dialougeRunner.StartDialogue(dialougeID);
    }


//---------------------------------
//STATIC FUNCTIONS BELOW
//---------------------------------

    private static bool FindMood(string mood, out MoodEnum foundEMood)
    {
        foreach (MoodEnum eMood in (Enum.GetValues(typeof(MoodEnum))))
        {
            if (eMood.ToString().Equals(mood))
            {
                foundEMood = eMood;
                return true;
            }
        }
        foundEMood = MoodEnum.Neutral;
        return false;
    }
    private static bool FindPerson(string person, out PersonsEnum foundEPerson) //schould not be able to change person without setting mood
    {
        foreach (PersonsEnum ePerson in (Enum.GetValues(typeof(PersonsEnum))))
        {
            if (ePerson.ToString().Equals(person))
            {
                foundEPerson = ePerson;
                return true;
            }
        }
        foundEPerson = PersonsEnum.Gwen;
        return false;

    }


//---------------------------------
//YARNSPINNER FUNCTIONS/COMMANDS BELOW
//---------------------------------

    [YarnFunction("change_person")]
    public static bool ChangePerson(string person, string mood)//used both for new character in same cutscene and starting new nodes
    {
        if (!FindPerson(person, out PersonsEnum foundEPerson)) {
            return false;
        }
        if(!FindMood(mood, out MoodEnum foundEMood)) { 
            return false; 
        }

        //send to cutSceneManager that it needs to change character
        CutsceneEventManager.ChangePersonEvent?.Invoke(foundEPerson, foundEMood);
        return true;
    }

    [YarnCommand("set_mood")]
    public static void SetMood(string mood) 
    {
        if (FindMood(mood, out MoodEnum foundEMood)) {
            CutsceneEventManager.SetMoodEvent?.Invoke(foundEMood);
        }
    }

    [YarnCommand("trigger_talk")]
    public static void TriggerTalk()
    {
        //send to cutSceneManager that current character needs to talk
        CutsceneEventManager.TriggerTalkingEvent?.Invoke();
    }

    /// <summary>
    /// for when you want the person to exit without swapping them for someone else
    /// </summary>
    [YarnCommand("trigger_exit")]
    public static void TriggerExit()
    {
        CutsceneEventManager.TriggerExitingEvent?.Invoke();
    }

    /// <summary>
    /// for when you want the person to enter without swapping them for someone else
    /// </summary>
    [YarnCommand("trigger_enter")]
    public static void TriggerEnter()
    {
        CutsceneEventManager.TriggerEnteringEvent?.Invoke();
    }

    [YarnCommand("end_of_it")]
    public static void EndOfIT()
    {
        EndOfDialougeEvent?.Invoke(false);
    }

    [YarnCommand("end_of_response")]
    public static void EndOfResponse()
    {
        EndOfDialougeEvent?.Invoke(true);
    }

    [YarnCommand("wait_until_entered")]
    public static IEnumerator WaitUntilEntered()
    {
        // Wait for 1 seconds (current time of enter animation)
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// DO NOT PUT AFTER END OF RESPONE OR END OF IT!!! THAT WILL CAUSE BUGS. Adds coroutine that makes yarn wait 0.5 seconds. 
    /// </summary>
    /// <returns></returns>
    [YarnCommand("wait_until_exited")]
    public static IEnumerator WaitUntilExited()  
    {
        // Wait for 1 second (current time of exit animation)
        yield return new WaitForSeconds(1f);
    }
}

public enum PersonsEnum
{
    Gwen,
    Bearmun,
    Hilare,
    Cressida,
    Berg,
    Thisle,
    Meredith
}

public enum MoodEnum
{
    Neutral,
    Happy,
    Angry
}
