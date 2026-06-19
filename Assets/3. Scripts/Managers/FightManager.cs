using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [Header("시스템")]
    public static FightManager fight;

    public Transform OurRange;
    public Transform EnemyRange;

    public event Action OnFightStarted; // 전투 돌입 시
    public event Action OnWaitStarted; // 명령 페이즈 시작
    public event Action OnTurnStarted; // 턴 시작 시

    [Header("UI")]
    public RectTransform upMage;
    public RectTransform downMage;

    public GameObject startP;
    public GameObject waitP;
    public GameObject turnP;

    Charactor nowselectedChar;

    public TMP_Text selectedname;



    private void Awake()
    {
        if (fight == null)
        {
            fight = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartStart()
    {
        startP.SetActive(true);

        CanvasGroup can = startP.GetComponent<CanvasGroup>();
        float second = 2f;

        StartCoroutine(UIMovement.UIMove.FadeOut(can, second));

        yield return new WaitForSeconds(second);

        OnWaitStarted?.Invoke();
    }


    IEnumerator WaitStart()
    {
        waitP.SetActive(false);

        StartCoroutine(SizeSetAnimation(upMage, new Vector2(1944, 94.3f)));
        StartCoroutine(SizeSetAnimation(upMage, new Vector2(1944, 272.3f)));

        yield return new WaitForSeconds(1.5f);

        waitP.SetActive(true);
    }

    IEnumerator SizeSetAnimation(RectTransform what, Vector2 target)
    {
        while (what.sizeDelta != target)
        {
            float x = Mathf.Lerp(what.sizeDelta.x, target.x, Time.deltaTime * 3);
            float y = Mathf.Lerp(what.sizeDelta.y, target.y, Time.deltaTime * 3);

            what.sizeDelta = new Vector2(x, y);
            yield return null;
        }
    }

    public void TurnStart()
    {
        OnTurnStarted?.Invoke();
    }

    public void SetSkillInfo()
    {

    }
}
