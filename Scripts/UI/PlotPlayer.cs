using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PlotSlide
{
    public Sprite image;
    [TextArea] public string text;
}

public class PlotPlayer : MonoBehaviour
{
    [Header("UI References")]
    public Image imageDisplay;
    public TMP_Text textDisplay;
    public CanvasGroup imageGroup;
    public CanvasGroup textGroup;

    [Header("Slide Content")]
    public List<PlotSlide> slides = new List<PlotSlide>();

    [Header("Transition Settings")]
    public float fadeDuration = 0.2f;
    public string nextSceneName = "Open menu";

    private int currentIndex = 0;
    private bool isTransitioning = false;

    void Start()
    {
        imageGroup.alpha = 0;
        textGroup.alpha = 0;
        ShowCurrentSlide();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning) // Left-click or tap
        {
            AdvanceSlide();
        }
    }

    void ShowCurrentSlide()
    {
        if (currentIndex >= slides.Count)
        {
            MusicManager.Instance.StopMusic();
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        PlotSlide slide = slides[currentIndex];
        imageDisplay.sprite = slide.image;
        textDisplay.text = slide.text;

        StartCoroutine(FadeInSlide());
    }

    IEnumerator FadeInSlide()
    {
        isTransitioning = true;

        imageGroup.alpha = 0;
        textGroup.alpha = 0;

        yield return StartCoroutine(FadeCanvasGroup(imageGroup, 0.2f, 1, fadeDuration));
        yield return StartCoroutine(FadeCanvasGroup(textGroup, 0.2f, 1, fadeDuration));

        isTransitioning = false;
    }

    void AdvanceSlide()
    {
        currentIndex++;
        ShowCurrentSlide();
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            cg.alpha = Mathf.Lerp(from, to, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cg.alpha = to;
    }
}
