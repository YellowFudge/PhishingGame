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
        button.onClick.AddListener(CompareAnswer);
    }

    private void OnDestroy() {
        button.onClick.RemoveListener(CompareAnswer);
    }

    //  Change to %, do math

    private void CheckCorrectTotal() {
        //Great Success
        if (totalCorrectMail == 7 || 8) {

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

        else { Debug.Log("Something went wrong"); }

        CheckCorrectQue();
    }

    private void CheckCorrectQue() {
        //Great Success
        if () {
            //76%+
        }

        else if () {
            //50 -> 75%
        }

        else if () {
            //25 -> 49%
        }

        else if () {
            //0 -> 24%
        }
    }
}
