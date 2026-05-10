using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   public void ChangeSceneName(string sceneName)
    {
        GameManager.sceneToLoad = sceneName;
        SceneManager.LoadScene("Loading Screen");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
