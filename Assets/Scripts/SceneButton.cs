using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    [SerializeField, Tooltip("The scene that you want to switch to")]
    private string sceneName;

    [SerializeField, Tooltip("If is Quit Game button")]
    private bool isQuit;

    [SerializeField, Tooltip("The time in seconds until the change will happen")]
    private float timeUntilChange;


    public void InvokeChangeScene()
    {
        Invoke("ChangeScene", timeUntilChange);
    }

    private void ChangeScene()
    {
        if (!isQuit)
        {


            int amountScenes = SceneManager.sceneCount;

            if (amountScenes > 1)
            {
                for (int i = 0; i < amountScenes; i++)
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                }
            }

            SceneManager.LoadScene(sceneName);
            return;
        }
        else
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

    }
}