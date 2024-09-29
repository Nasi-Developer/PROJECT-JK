using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonBase<SceneController>
{
    public int LastSceneBuildIndex = -1;

    public void SaveScene(int sceneBuildIndex)
    {
        LastSceneBuildIndex = sceneBuildIndex;
    }

    public void LoadScene(int sceneBuildIndex)
    {
        LastSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void LoadScene(string SceneName)
    {
        LastSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(SceneName);
    }
}
