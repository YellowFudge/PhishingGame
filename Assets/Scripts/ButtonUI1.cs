using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonUI1 : MonoBehaviour {
  //[SerializeField] private string ButtonOutput = "Hello World";
  [SerializeField] private TMP_Text targetText;

  public GameObject pauseUI;
  public GameObject contactsUI;
  public GameObject companiesUI;
  public GameObject deliveryUI;
  public GameObject calanderUI;

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

  // Open Contacts
  public void OnContactsPress() {
    contactsUI.SetActive(true);
    companiesUI.SetActive(false);
    deliveryUI.SetActive(false);
    calanderUI.SetActive(false);
  }
  public void OnCompaniesPress() {
    contactsUI.SetActive(false);
    companiesUI.SetActive(true);
    deliveryUI.SetActive(false);
    calanderUI.SetActive(false);
  }
  public void OnDeleveryPress() {
    contactsUI.SetActive(false);
    companiesUI.SetActive(false);
    deliveryUI.SetActive(true);
    calanderUI.SetActive(false);
  }
  public void OnCalanderPress() {
    contactsUI.SetActive(false);
    companiesUI.SetActive(false);
    deliveryUI.SetActive(false);
    calanderUI.SetActive(true);
  }
}
