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

    /// <summary>
    /// triggers coroutine that waits 0.1 seconds. Used so that yarnspinner can escape the current node before any others have the possibility to be called
    /// </summary>
    /// <param name="isEndOfResponse">true if is end of response/evaluation cutscene. false if end of IT cutscene</param>
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
            //event used by cutSceneManager
            CutsceneEventManager.EndOfITDialougeEvent?.Invoke();
        }  
    }
    /// <summary>
    /// For triggering dialouge cutscenes of a given ID. Currently doesn't check if a node is already running
    /// </summary>
    /// <param name="dialougeID">the title of the node which's cutscene to start</param>
    public void StartDialouge(string dialougeID)
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


    /// <summary>
    /// Selects a new person to show on screen and sets their mood. Moves out the previous one if one was in the frustum.
    /// </summary>
    /// <param name="person">The person's name, matching one in the PersonsEnum</param>
    /// <param name="mood">The person's mood, matching one in the MoodEnum</param>
    /// <returns>true if both mood and person were found,  false if not</returns>
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

    /// <summary>
    /// Sets the mood of the currently selected person
    /// </summary>
    /// <param name="mood">The person's mood, matching one in the MoodEnum</param>
    [YarnCommand("set_mood")]
    public static void SetMood(string mood) 
    {
        if (FindMood(mood, out MoodEnum foundEMood)) {
            CutsceneEventManager.SetMoodEvent?.Invoke(foundEMood);
        }
    }

    /// <summary>
    /// Triggers the currently selected person's talk animation to run once
    /// </summary>
    [YarnCommand("trigger_talk")]
    public static void TriggerTalk()
    {
        //send to cutSceneManager that current character needs to talk
        CutsceneEventManager.TriggerTalkingEvent?.Invoke();
    }

    /// <summary>
    /// Causes the currently selected person to exit without swapping them for someone else
    /// </summary>
    [YarnCommand("trigger_exit")]
    public static void TriggerExit()
    {
        CutsceneEventManager.TriggerExitingEvent?.Invoke();
    }

    /// <summary>
    /// Causes the currently selected person to enter without swapping them for someone else
    /// </summary>
    [YarnCommand("trigger_enter")]
    public static void TriggerEnter()
    {
        CutsceneEventManager.TriggerEnteringEvent?.Invoke();
    }

    /// <summary>
    /// MUST be placed as last line if used in a yarnspinner node. Sends event alerting that the end of an IT cutscene has been reached
    /// </summary>
    [YarnCommand("end_of_it")]
    public static void EndOfIT()
    {
        EndOfDialougeEvent?.Invoke(false);
    }

    /// <summary>
    /// MUST be placed as last line if used in a yarnspinner node. Sends an event alerting that the end of an evaluation/response cutscene has been reached
    /// </summary>
    [YarnCommand("end_of_response")]
    public static void EndOfResponse()
    {
        EndOfDialougeEvent?.Invoke(true);
    }

    /// <summary>
    /// Makes yarnspinner script wait for the amount of time it takes for a person's enter animation to play before continuing to the next line
    /// </summary>
    /// <returns></returns>
    [YarnCommand("wait_until_entered")]
    public static IEnumerator WaitUntilEntered()
    {
        // Wait for 1 seconds (current time of enter animation)
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// Makes yarnspinner script wait for the amount of time it takes for a person's exit animation to play before continuing to the next line 
    /// </summary>
    /// <returns></returns>
    [YarnCommand("wait_until_exited")]
    public static IEnumerator WaitUntilExited()  
    {
        // Wait for 1 second (current time of exit animation)
        yield return new WaitForSeconds(1f);
    }
}
