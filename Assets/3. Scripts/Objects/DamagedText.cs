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

    // Start is called before the first frame update
    void Start()
    {
        mytext = GetComponent<TMP_Text>();
        mytext.text = "-" + damage.ToString("000");
        childtext = transform.GetChild(0).GetComponent<TMP_Text>();
        childtext.text = "-" + damage.ToString("000");

        rec = GetComponent<RectTransform>();
        /*

        float x = 0f;
        if (isright)
        {
            x = 70f;
        }
        else
        {
            x = -70f;
        }
        rec.position = new Vector2(rec.position.x + x, rec.position.y + 80f);
                */
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

        float path = 0f;
        if(isright)
        {
            path = 70f;
        }
        else
        {
            path = -70f;
        }

        StartCoroutine(UIMovement.MovingAnimation(rec, new Vector2(rec.anchoredPosition.x + path, rec.anchoredPosition.y), 1.5f));

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
