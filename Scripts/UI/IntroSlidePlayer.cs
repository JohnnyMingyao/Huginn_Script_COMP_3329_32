using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class IntroSlide
{
    public Sprite image;
    [TextArea] public string text;
}

public class IntroSlidePlayer : MonoBehaviour
{
    [Header("UI References")]
    public Image imageDisplay;
    public TMP_Text textDisplay;
    public CanvasGroup imageGroup;
    public CanvasGroup textGroup;

    [Header("Slide Config")]
    public List<IntroSlide> slides = new List<IntroSlide>();
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    [Header("Scene Settings")]
    public string nextSceneName = "Open menu";

    void Start()
    {
        StartCoroutine(PlaySlides());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter)||Input.GetKeyDown(KeyCode.Z )||Input.GetKeyDown(KeyCode.Space)){
            MusicManager.Instance.StopMusic();
            SceneManager.LoadScene(nextSceneName);
        }
    }

    IEnumerator PlaySlides()
    {
        imageGroup.alpha = 0;
        textGroup.alpha = 0;

        foreach (var slide in slides)
        {
            // Set content
            imageDisplay.sprite = slide.image;
            textDisplay.text = slide.text;

            // Fade in
            yield return StartCoroutine(FadeCanvasGroup(imageGroup, 0, 1, fadeDuration));
            yield return StartCoroutine(FadeCanvasGroup(textGroup, 0, 1, fadeDuration));

            // Hold
            yield return new WaitForSeconds(displayDuration);

            // Fade out
            yield return StartCoroutine(FadeCanvasGroup(textGroup, 1, 0, fadeDuration));
            yield return StartCoroutine(FadeCanvasGroup(imageGroup, 1, 0, fadeDuration));
        }

        // Done → load next scene
        MusicManager.Instance.StopMusic();
        SceneManager.LoadScene(nextSceneName);
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
