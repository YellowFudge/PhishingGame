using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonUI1 : MonoBehaviour {
  //[SerializeField] private string ButtonOutput = "Hello World";
  //[SerializeField] private TMP_Text targetText;
  //[SerializeField] private GameObject[] panels;

  public GameObject pauseUI;
  public GameObject contactsUI;
  public GameObject companiesUI;
  public GameObject deliveryUI;
  public GameObject calanderUI;
  bool pauseOpen = false;

  public GameObject checkboxUI;

  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape) && !pauseOpen) {
      OnEnterPausePress();
    } else if (Input.GetKeyDown(KeyCode.Escape) && pauseOpen) {
      OnGameResumePress();
    }
  }
  // Open Pause Menu
  public void OnEnterPausePress() {
    pauseUI.SetActive(true);
    pauseOpen = true;
  }

  // Close Pause Menu
  public void OnGameResumePress() {
    pauseUI.SetActive(false);
    pauseOpen = false;
  }

  // Exit game through pause menu
  public void OnGameExitPress() {
    Application.Quit();
  }

  public void OnCheckboxOpenPress() {
    checkboxUI.SetActive(true);
  }
}
