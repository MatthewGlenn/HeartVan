using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private int currentSceneIndex;
    [SerializeField] private int MainMenuSceneIndex = 0;
    public static Scene currentScene { get; set; }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string SceneName)
    {
        if (SceneNameCheck(SceneName))
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private bool SceneNameCheck(string sceneName)
    {
        string scenePath = "Assets/Scenes/" + sceneName + ".unity";
        if (SceneUtility.GetBuildIndexByScenePath(scenePath) >= 0) {
            return true;
        }
        Debug.LogError("Scene does not exist: " + sceneName);
        return false;
    }
}