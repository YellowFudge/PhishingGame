using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavManager : MonoBehaviour
{
  public void MoveToScene() {
    SceneManager.LoadScene("EdvinsTestScene");
  }
  public void ExitGame() {
    Application.Quit(); 
  }
}
