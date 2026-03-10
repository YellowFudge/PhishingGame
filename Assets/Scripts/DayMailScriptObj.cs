using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Daily Mail List")]
public class DailyMailArrayScriptObj : ScriptableObject
{
    [SerializeField] DailyMails[] dailyMails;

    public DailyMails[] DailyMails {  get { return dailyMails; } }
    public int GetNumOfDays() 
    { 
        return dailyMails.Length;
    }

    public DailyMails GetTodaysMails(int dayNum)
    {
        return dailyMails[dayNum-1];
    }

    public bool GetCurrentMail(int dayNum, int mailNum, out GameObject mail)
    {
        if(dailyMails.Length > dayNum-1 && dailyMails[dayNum-1].mailPrefabs.Length > mailNum-1)
        {
            mail = dailyMails[dayNum - 1].mailPrefabs[mailNum-1]; 
            return true;
        }
        mail = null;
        return false;
    }

    public bool GetCurrentMailinfo(int dayNum, int mailNum, out MailCueTypes mailCues)
    {
        if(dailyMails.Length > dayNum-1 && dailyMails[dayNum-1].mailPrefabs.Length > mailNum-1)
        {
            GameObject mail = dailyMails[dayNum - 1].mailPrefabs[mailNum-1]; 
            mailCues = mail.GetComponent<MailCueTypes>(); //check if false too

            return true;
        }
        mailCues = null;
        return false;
    }


}

[Serializable]
public class DailyMails
{
    public GameObject[] mailPrefabs;
}
