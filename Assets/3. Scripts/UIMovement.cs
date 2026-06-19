using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    public static UIMovement UIMove;

    private void Awake()
    {
        if(UIMove == null)
        {
            UIMove = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FadeIn(CanvasGroup what, float fadeTime)
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

    public IEnumerator FadeOut(CanvasGroup what, float fadeTime)
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
}
