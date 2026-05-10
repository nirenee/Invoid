using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Asyncloader : MonoBehaviour
{
    public Slider loadingSlider;

    IEnumerator Start()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(GameManager.sceneToLoad);
        op.allowSceneActivation = false;
        float elapsed = 0f;
        float minTime = 1.5f;
        while (op.progress < 0.9f || elapsed < minTime)
        {
            loadingSlider.value = Mathf.Clamp01(op.progress / 0.9f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        op.allowSceneActivation = true;
    }

}
