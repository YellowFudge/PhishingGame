using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonUI1 : MonoBehaviour {
  //[SerializeField] private string ButtonOutput = "Hello World";
  [SerializeField] private TMP_Text targetText;

  public GameObject pauseUI;

  public void ChangeText() {        
    targetText.text = "Button was pressed!";
  }

  // Open Pause Menu
  public void OnEnterPausePress() {
    pauseUI.SetActive(true);
  }

  // Close Pause Menu
  public void OnGameResumePress() {
    pauseUI.SetActive(false);
  }

  // Exit game through pause menu
  public void OnGameExitPress() {
    Application.Quit();
  }
}
