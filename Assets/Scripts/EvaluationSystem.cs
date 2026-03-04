using UnityEngine;
using UnityEngine.UI;

public class EvaluationSystem : MonoBehaviour
{
    public Button button;

    private void Start() {
        button.onClick.AddListener(CompareAnswer);
    }

    private void OnDestroy() {
        button.onClick.RemoveListener(CompareAnswer);
    }
}
