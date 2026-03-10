using UnityEngine;
using UnityEngine.UI;

public class EvaluationSystem : MonoBehaviour
{
    public Button button;

    /* Temporary variables */
    public int totalCorrectMail = 8;
    public int totalMail = 8;

    public int totalPlayerQuePoints = 2000;
    public int totalQuePoints = 2400;

    public int valueA;
    public int valueB;

    private void Start() {
        button.onClick.AddListener(CheckCorrectTotal);
    }

    private void OnDestroy() {
        button.onClick.RemoveListener(CheckCorrectTotal);
    }

    //  Change to %, do math

    public void CheckCorrectTotal() {
        double greatSuccessMail = totalMail * 0.75;
        double minorSuccessMail = totalMail * 0.50;
        double minorFailureMail = totalMail * 0.25;

        Debug.Log("greatSuccess: " + greatSuccessMail + "+ minorSuccess: " + minorSuccessMail + "+ minorFailure: " + minorFailureMail + "+ majorFailure: <" + minorFailureMail);

        //Great Success 75% +
        if (totalCorrectMail >= greatSuccessMail)
        { //75%+
            Debug.Log("75%+ Great Success: " + totalCorrectMail + " points out of: " + totalMail);
            valueA = 4;
        }

        else if (totalCorrectMail >= minorSuccessMail || totalCorrectMail < greatSuccessMail)
        { //50 -> 75%
            Debug.Log("50% -> <75% Minor Success: " + totalCorrectMail + " points out of: " + totalMail);
            valueA = 3;
        }

        else if (totalCorrectMail >= minorFailureMail || totalCorrectMail < minorSuccessMail)
        { //25 -> 49%
            Debug.Log("25% -> <50% Minor Failure: " + totalCorrectMail + " points out of: " + totalMail);
            valueA = 2;
        }

        else if (totalCorrectMail < minorFailureMail)
        { //less than 25%-
            Debug.Log("<25% -> 0% Great Failure: " + totalCorrectMail + " points out of: " + totalMail);
            valueA = 1;
        }
        else { Debug.Log("Something went horrible wrong"); }

        CheckCorrectQue();
    }

    private void CheckCorrectQue() {
        double greatSuccessQue = totalQuePoints * 0.75;
        double minorSuccessQue = totalQuePoints * 0.50;
        double minorFailureQue = totalQuePoints * 0.25;

        Debug.Log("greatSuccess: " + greatSuccessQue + "+ minorSuccess: " + minorSuccessQue + "+ minorFailure: " + minorFailureQue + "+ majorFailure: <" + minorFailureQue);

        //Great Success 75% +
        if (totalPlayerQuePoints >= greatSuccessQue)
        { //75%+
            Debug.Log("75%+ Great Success: " + totalPlayerQuePoints + " points out of: " + totalQuePoints);
            valueB = 4;
        }

        else if (totalPlayerQuePoints >= minorSuccessQue || totalPlayerQuePoints < greatSuccessQue)
        { //50 -> 75%
            Debug.Log("50% -> <75% Minor Success: " + totalPlayerQuePoints + " points out of: " + totalQuePoints);
            valueB = 3;
        }

        else if (totalPlayerQuePoints >= minorFailureQue || totalPlayerQuePoints < minorSuccessQue)
        { //25 -> 49%
            Debug.Log("25% -> <50% Minor Failure: " + totalPlayerQuePoints + " points out of: " + totalQuePoints);
            valueB = 2;
        }

        else if (totalPlayerQuePoints < minorFailureQue)
        { //less than 25%-
            Debug.Log("<25% -> 0% Great Failure: " + totalPlayerQuePoints + " points out of: " + totalQuePoints);
            valueB = 1;
        }
        else { Debug.Log("Something went horrible wrong"); }

        ChooseBossResponse();
    }

    // Function to choose response
    private void ChooseBossResponse()
    {
        // Map out the responses
        // Day x (level x) + valueA (correct mail types value) + valueB (correct que types)
        // This makes it so we need per level: A x B amount of responses (in this case 16, can easily be changed)
    }
}
