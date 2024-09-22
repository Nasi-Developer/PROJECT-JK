using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_UI : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void OnClickStartButton()
    {
        Loading_UI.Instance.Show();
        Loading_UI.Instance.LoadingProgress = 0f;

        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        var async = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            Loading_UI.Instance.LoadingProgress = async.progress / 0.9f;
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));

        var asyncunload = SceneManager.UnloadSceneAsync(1);

        while (!asyncunload.isDone)
        {
            yield return null;
        }

        Loading_UI.Instance.LoadingProgress = 1f;
        Loading_UI.Instance.Hide();
        gameObject.SetActive(false);
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }
}
