using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonBase<SceneController>
{
    public int LastSceneBuildIndex;
    public void LoadScene(int SceneNumber)
    {
        LastSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(SceneNumber);
    }

    public void LoadScene(string SceneName)
    {
        LastSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(SceneName);
    }
}
