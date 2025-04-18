using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Button : MonoBehaviour
{
    public string sceneName;
    public int level = -1;
    public float delay = 0f;

    public IEnumerator Action() {
        yield return new WaitForSeconds(delay);
        if (!string.IsNullOrEmpty(sceneName)) {
            SceneManager.LoadScene(sceneName);
        }
        if (level >= 0) {
            LevelManager.StartLevel(level);
        }
    }

}