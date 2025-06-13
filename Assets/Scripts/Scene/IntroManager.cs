using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Transform xrRig;
    public CanvasGroup canvas;
    public float fadeDuration = 2f;

    [Header("Panels")]
    public GameObject first;
    public GameObject second;

    private bool readyToContinue = false;
    private int buttonPressCount = 0;

    void Start()
    {
        canvas.alpha = 0f;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
        first.SetActive(true);
        second.SetActive(false);
        StartCoroutine(Sequence());
    }

    void Update()
    {
        if (readyToContinue)
        {
            if (Input.anyKeyDown)
            {
                HandleButtonPress();
            }
        }
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(5.5f);

        yield return StartCoroutine(SmoothRotateXR(-180f, 2f));

        yield return new WaitForSeconds(1f);

        StartCoroutine(FadeInCanvas());
    }

    IEnumerator SmoothRotateXR(float degrees, float duration)
    {
        float elapsed = 0f;

        Quaternion startRotation = xrRig.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, degrees, 0);

        while (elapsed < duration)
        {
            xrRig.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        xrRig.rotation = endRotation;
    }

    IEnumerator FadeInCanvas()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            canvas.alpha = timer / fadeDuration;
            timer += Time.deltaTime;
            yield return null;
        }

        canvas.alpha = 1f;

        readyToContinue = true;
    }

    void HandleButtonPress()
    {
        buttonPressCount++;

        if (buttonPressCount == 1)
        {
            first.SetActive(false);
            second.SetActive(true);
        }
        else if (buttonPressCount >= 2)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
