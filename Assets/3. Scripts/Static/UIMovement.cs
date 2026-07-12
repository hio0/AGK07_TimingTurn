using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class UIMovement
{
    public static IEnumerator FadeIn(CanvasGroup what, float fadeTime)
    {
        float time = 0f;
        what.gameObject.SetActive(true);
        what.alpha = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            what.alpha = Mathf.Lerp(0f, 1f, time / fadeTime);
            yield return null;
        }

        what.alpha = 1f;
    }

    public static IEnumerator FadeOut(CanvasGroup what, float fadeTime)
    {
        float time = 0f;
        what.gameObject.SetActive(true);
        what.alpha = 1f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            what.alpha = Mathf.Lerp(1f, 0f, time / fadeTime);
            yield return null;
        }

        what.alpha = 0f;
        what.gameObject.SetActive(false);
    }

    public static IEnumerator SizeSetAnimation(RectTransform what, Vector2 target, float speed)
    {
        while ((what.sizeDelta - target).sqrMagnitude > 0.001f)
        {
            float x = Mathf.Lerp(what.sizeDelta.x, target.x, Time.deltaTime * speed);
            float y = Mathf.Lerp(what.sizeDelta.y, target.y, Time.deltaTime * speed);

            what.sizeDelta = new Vector2(x, y);
            yield return null;
        }
    }

    public static IEnumerator MovingAnimation(RectTransform what, Vector2 target, float speed)
    {
        while ((what.anchoredPosition - target).sqrMagnitude > 0.001)
        {
            float x = Mathf.Lerp(what.anchoredPosition.x, target.x, Time.deltaTime * speed);  
            float y = Mathf.Lerp(what.anchoredPosition.y, target.y, Time.deltaTime * speed);

            what.anchoredPosition = new Vector2(x, y);
            yield return null;
        }
    }

    public static IEnumerator MovingAnimationWorld(RectTransform what, Vector3 target, float speed)
    {
        while ((what.position - target).sqrMagnitude > 0.001)
        {
            float x = Mathf.Lerp(what.position.x, target.x, Time.deltaTime * speed);
            float y = Mathf.Lerp(what.position.y, target.y, Time.deltaTime * speed);

            what.position = new Vector2(x, y);
            yield return null;
        }
    }
}
