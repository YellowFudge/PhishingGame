using UnityEngine;

public class PanelManager : MonoBehaviour {
  [SerializeField] private GameObject[] panels;

  // General panel manager.
  // Each button passes the panel name when ShowPanel is called.
  // ShowPanel closes all active windows in panels[] and activates the requested window.
  public void ShowPanel(GameObject panelToShow) {
    foreach (GameObject panel in panels) {
       panel.SetActive(false);
    }
    panelToShow.SetActive(true);
  }
  public void ShowPausePanel() {
      
  }
}
