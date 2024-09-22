using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Loading_UI : MonoBehaviour
{
    public static Loading_UI Instance { get ; private set; }
    public float LoadingProgress
    {
        get => progress;
        set
        {
            progress = value;
            progresstext.text = $"{progress * 100f:0}%";
            progressImage.fillAmount = progress;
        }
    }
        

    public TextMeshProUGUI progresstext;
    private float progress = 0f;
    public Image progressImage;

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);  
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
