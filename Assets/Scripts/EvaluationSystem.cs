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

    private void Start() {
        button.onClick.AddListener(CheckCorrectTotal);
    }

    private void OnDestroy() {
        button.onClick.RemoveListener(CheckCorrectTotal);
    }

    //  Change to %, do math

    private void CheckCorrectTotal() {
        //Great Success
        /*if (totalCorrectMail == 7 || 8) {

        }

        //Minor Success
        else if (totalCorrectMail == 5 || 6) {

        }

        //Minor Failure
        else if (totalCorrectMail == 3 || 4) {

        }

        //Great Failure
        else if (totalCorrectMail <= 2) {

        }

        else { Debug.Log("Something went wrong"); }*/

        CheckCorrectQue();
    }

    private void CheckCorrectQue() {
        double greatSuccess = totalQuePoints * 0.75;
        double minorSuccess = totalQuePoints * 0.50;
        double minorFailure = totalQuePoints * 0.25;

        Debug.Log("greatSuccess: " + greatSuccess + "+ minorSuccess: " + minorSuccess + "+ minorFailure: " + minorFailure + "+ majorFailure: <" + minorFailure);

        //Great Success 75% +
        if (totalPlayerQuePoints >= greatSuccess)
        { //75%+
            Debug.Log("75%+ Great Success: " + totalPlayerQuePoints + " points out of: " + totalQuePoints);
        }

        else if (totalPlayerQuePoints >= minorSuccess || totalPlayerQuePoints < greatSuccess)
        { //50 -> 75%
            Debug.Log("50% -> <75% Minor Success: " + totalPlayerQuePoints + " points out of: " + totalQuePoints);
        }

        else if (totalPlayerQuePoints >= minorFailure || totalPlayerQuePoints < minorSuccess)
        { //25 -> 49%
            Debug.Log("25% -> <50% Minor Failure: " + totalPlayerQuePoints + " points out of: " + totalQuePoints);
        }

        else if (totalPlayerQuePoints < minorFailure)
        { //less than 25%-
            Debug.Log("<25% -> 0% Great Failure: " + totalPlayerQuePoints + " points out of: " + totalQuePoints);
        }
        else { Debug.Log("Something went horrible wrong"); }
    }
}
