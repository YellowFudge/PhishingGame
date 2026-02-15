using UnityEngine;

public class PanelManager : MonoBehaviour {
  [SerializeField] private GameObject[] panels;
  bool checkOpen = false;

  void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      //panels[0].SetActive(true);
      ShowPanel(panels[0]);
    } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
      ShowPanel(panels[1]);
    } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
      ShowPanel(panels[2]);
    } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
      ShowPanel(panels[3]);
      //panels[1].SetActive(true)
    }
    if (Input.GetKeyDown(KeyCode.Space)) {
      if (!checkOpen) {
        panels[4].SetActive(true);
        checkOpen = true;
      } else if (checkOpen) {
        panels[4].SetActive(false);
        checkOpen = false;
      }
    }
  }
  // General panel manager.
  // Each button passes the panel name when ShowPanel is called.
  // ShowPanel closes all active windows in panels[] and activates the requested window.
  public void ShowPanel(GameObject panelToShow) {
    foreach (GameObject panel in panels) {
       panel.SetActive(false);
    }
    panelToShow.SetActive(true);
  }
  // public void ShowPausePanel() {}
}
