using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagedText : MonoBehaviour
{
    public int damage;
    public bool isright;
    TMP_Text mytext;
    TMP_Text childtext;
    RectTransform rec;

    public void Instalize(int dam, bool right)
    {
        damage = dam;
        isright = right;
    }

    // Start is called before the first frame update
    void Start()
    {
        mytext = GetComponent<TMP_Text>();
        mytext.text = "-" + damage.ToString("000");
        childtext = transform.GetChild(0).GetComponent<TMP_Text>();
        childtext.text = "-" + damage.ToString("000");

        rec = GetComponent<RectTransform>();

        float x = 150f;
        if (!isright)
        {
            x = -x;
        }
        rec.anchoredPosition = new Vector2(rec.anchoredPosition.x + x, Mathf.Abs(rec.anchoredPosition.y));

        StartCoroutine(Animation(damage));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Animation(int changed)
    {
        CanvasGroup can = GetComponent<CanvasGroup>();
        float time = 0f;
        float fadeout = 2f;

        can.alpha = 1f;

        float path = 50f;
        if(!isright)
        {
            path = -path;
        }
        StartCoroutine(UIMovement.MovingAnimation(rec, new Vector2(rec.anchoredPosition.x + path, rec.anchoredPosition.y), 0.5f));

        while (time < fadeout)
        {
            time += Time.deltaTime;
            can.alpha = Mathf.Lerp(1f, 0f, time / fadeout);
            yield return null;
        }

        can.alpha = 0f;
        Destroy(gameObject);
    }
}
