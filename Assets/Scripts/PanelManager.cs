using UnityEngine;

public class PanelManager : MonoBehaviour {
  [SerializeField] private GameObject[] panels;

  public void ShowPanel(GameObject panelToShow) {
    foreach (GameObject panel in panels) {
       panel.SetActive(false);
    }
    panelToShow.SetActive(true);
  }
}
