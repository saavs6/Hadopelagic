using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Button : MonoBehaviour
{
    public string sceneName;

    public void Action() {
        if (!string.IsNullOrEmpty(sceneName)) {
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay() {
        yield return new WaitForSeconds(1.5f);
        LevelManager.StartLevel(1);
        SceneManager.LoadScene(sceneName);
    }
}