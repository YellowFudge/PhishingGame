using System;
using UnityEngine;

public class PersonManager : MonoBehaviour
{
    [SerializeField] PersonAnimator[] personAnimArray;

    int _currentPersonIndex;
    bool _currentPersonInFrame; //to ensure that persons' don't accidentallly run exit and enter anims when already at desired position

    private void Awake()
    {
        _currentPersonIndex = -1;
        _currentPersonInFrame = false;
    }

    public void EndOfDialouge()
    {
        //move current person out of frame
        personAnimArray[_currentPersonIndex].animator.SetTrigger("Exit");
        _currentPersonIndex = -1;
        _currentPersonInFrame = false;
    }

    public void TriggerTalkAnim()
    {
        personAnimArray[_currentPersonIndex].animator.SetTrigger("Talk");
    }

    public void TriggerExitAnim()
    {
        if (!_currentPersonInFrame) return;

        personAnimArray[_currentPersonIndex].animator.SetTrigger("Exit");
        _currentPersonInFrame = false;
    }

    public void TriggerEnterAnim()
    {
        if (_currentPersonInFrame) return;
        //move them into frame
        personAnimArray[_currentPersonIndex].animator.SetTrigger("Enter");
        _currentPersonInFrame = true;
    }

    public void SetMood(MoodEnum mood)
    {
        //unset all in current person's animator
        personAnimArray[_currentPersonIndex].animator.SetBool("IsHappy", false);
        personAnimArray[_currentPersonIndex].animator.SetBool("IsNeutral", false);
        personAnimArray[_currentPersonIndex].animator.SetBool("IsAngry", false);

        //set the specific feeling you want
        personAnimArray[_currentPersonIndex].animator.SetBool($"Is{mood.ToString()}", true);//this might not work
    }

    public void ChangePerson(PersonsEnum person, MoodEnum mood)
    {
        bool isSame = false;
        //replace current person with the new current person
        if (!FindPersonIndex(person, out int personIndex))
        {
            Debug.LogError("Could not find animator connected to requested person, add to array");
            return;
        }

        if (_currentPersonIndex.Equals(personIndex))
        {
            isSame = true;
        }

        if (!_currentPersonIndex.Equals(-1) && !isSame && _currentPersonInFrame)
        {
            //move current person out of frame  
            TriggerExitAnim();
        }

        _currentPersonIndex = personIndex;

        //making sure is enabled (if someone turned them off)
        personAnimArray[_currentPersonIndex].animator.gameObject.SetActive(true);

        //set mood
        SetMood(mood);

        if (!isSame)
        {
            //move them into frame
            TriggerEnterAnim();
        }
    }


    bool FindPersonIndex(PersonsEnum person, out int personIndex)
    {
        for (int i = 0; i < personAnimArray.Length; i++)
        {
            if (personAnimArray[i].ePerson.Equals(person))
            {
                personIndex = i;
                return true;
            }
        }
        personIndex = -1;
        return false;
    }

}

[Serializable]
public class PersonAnimator
{
    public PersonsEnum ePerson;
    public Animator animator;
}