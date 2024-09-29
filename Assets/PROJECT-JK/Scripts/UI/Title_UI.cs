using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_UI : MonoBehaviour
{
    public static Title_UI Instance {  get; private set; }
    public TextMeshProUGUI TouchText;
    public Image TouchTextImage;
    public System.Action PlayStartSound;
    private bool TouchTextHide = true;
    private float FadeInOutTime = 1.0f;

    private void Awake()
    {
        TouchText.gameObject.SetActive(false);
        TouchTextImage.gameObject.SetActive(false);
        Instance = this;
    }

    private void Start()
    {
        VideoController.Instance.ShowTouchStartText += ShowTouchText;
        SceneController.Singleton.SaveScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (!TouchTextHide && Input.GetMouseButtonDown(0))
        {
            SceneController.Singleton.LoadScene(1);
        }
    }

    private void ShowTouchText()
    {
        TouchText.gameObject.SetActive(true);
        TouchTextImage.gameObject.SetActive(true);
        PlayStartSound?.Invoke();
        StartCoroutine(FadeInOutText());
        TouchTextHide = false;
    }

    IEnumerator FadeInOutText()
    {
        if (TouchText == null || TouchTextImage == null)
        {
            yield break;
        }

        while (true)
        {
            yield return StartCoroutine(TextAlphaUpdate(0f));

            yield return StartCoroutine(TextAlphaUpdate(0.75f));
        }
    }

    IEnumerator TextAlphaUpdate(float TargetAlpha)
    {
        float currentTextAlpha = TouchText.alpha;
        Color color = TouchTextImage.color;
        float currentImageAlpha = color.a;
        float FadeInOutDeltaTime = 0f;

        while (FadeInOutDeltaTime < FadeInOutTime)
        {
            FadeInOutDeltaTime += Time.deltaTime;

            TouchText.alpha = Mathf.Lerp(currentTextAlpha, TargetAlpha, FadeInOutDeltaTime / FadeInOutTime);
            color.a = Mathf.Lerp(currentImageAlpha, TargetAlpha, FadeInOutDeltaTime / FadeInOutTime);
            TouchTextImage.color = color;

            yield return null;
        }

    }
}
