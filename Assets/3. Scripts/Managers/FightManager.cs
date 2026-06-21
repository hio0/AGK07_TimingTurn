using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class FightManager : MonoBehaviour
{
    [Header("시스템")]
    public static FightManager fight;

    public Transform OurRange;
    public Transform EnemyRange;

    public Dictionary<float, Skill> skillList = new Dictionary<float, Skill>();

    public event Action OnFightStarted; // 전투 돌입 시
    public event Action OnWaitStarted; // 명령 페이즈 시작
    public event Action OnTurnStarted; // 턴 시작 시
    public event Action OnTurnFinished; // 턴 끝날 시

    [Header("UI")]
    public RectTransform upMage;
    public RectTransform downMage;

    public GameObject startP;
    public GameObject waitP;
    public GameObject turnP;

    public RectTransform garimmack;
    int turncount;
    public TMP_Text turnT;

    int unitselectednum;
    Unit nowselectedUnit;
    public Transform skills_transform;
    public Image pre_skillicon;

    public TMP_Text selectedname;
    public Transform actcounts_transform;
    public GameObject pre_actcounticon;
    public GameObject skillblabla;
    public TMP_Text skillname;
    public TMP_Text skilltype;
    public TMP_Text skillexplanation;
    public Transform blablaacs_transform;



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
        Action wait = () => StartCoroutine(WaitStart());
        OnWaitStarted += wait;

        Action fight = () =>
        {
            StartCoroutine(StartStart());
            turncount = 1;
        };
        OnFightStarted += fight;

        Action end = () => turncount++;
        OnTurnFinished += end;

        OnFightStarted?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitP.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (unitselectednum > 0)
                {
                    NowSelectUnit(unitselectednum - 1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (unitselectednum < OurRange.childCount - 1)
                {
                    NowSelectUnit(unitselectednum + 1);
                }
            }
        }
    }

    IEnumerator StartStart()
    {
        startP.SetActive(true);
        waitP.SetActive(false);

        upMage.sizeDelta = new Vector2(1944f, 0f);
        downMage.sizeDelta = new Vector2(1944f, 0f);

        garimmack.sizeDelta = new Vector2(273.4f, 66.94f);
        garimmack.gameObject.SetActive(true);
        StartCoroutine(SizeSetAnimation(garimmack, new Vector2(273.4f, 30f), 2f));

        yield return new WaitForSeconds(2f);
        garimmack.gameObject.SetActive(false);

        CanvasGroup can = startP.GetComponent<CanvasGroup>();
        float second = 2f;

        StartCoroutine(UIMovement.UIMove.FadeOut(can, second));

        yield return new WaitForSeconds(second + 0.2f);

        OnWaitStarted?.Invoke();
    }


    IEnumerator WaitStart()
    {
        waitP.SetActive(false);

        StartCoroutine(SizeSetAnimation(upMage, new Vector2(1944, 94.3f), 3.5f));
        StartCoroutine(SizeSetAnimation(downMage, new Vector2(1944, 272.3f), 3.5f));

        yield return new WaitForSeconds(1.5f);

        waitP.SetActive(true);
        turnT.text = $"<size=60><b>{turncount}</b></size>Turn";

        unitselectednum = OurRange.childCount - 1; // 항상 맨 앞의 놈부터 보여줄것
        NowSelectUnit(OurRange.childCount - 1);
    }

    public void NowSelectUnit(int now)
    {
        OurRange.GetChild(unitselectednum).gameObject.transform.Find("Select").gameObject.SetActive(false);

        unitselectednum = now;

        GameObject b = OurRange.GetChild(unitselectednum).gameObject;
        nowselectedUnit = b.GetComponent<Unit>();
        b.transform.Find("Select").gameObject.SetActive(true);

        SetWaitPanel();
    }

    void SetWaitPanel()
    {
        if (skills_transform.childCount != 0)
        {
            for (int i = 0; i < skills_transform.childCount; i++)
            {
                Destroy(skills_transform.GetChild(i).gameObject);
            }
        }

        if (actcounts_transform.childCount != 0)
        {
            for (int i = 0; i < actcounts_transform.childCount; i++)
            {
                Destroy(actcounts_transform.GetChild(i).gameObject);
            }
        }

        selectedname.text = nowselectedUnit.charactorName;

        for (int i = 0; i < nowselectedUnit.skills.Length; i++)
        {
            Image b = Instantiate(pre_skillicon, skills_transform);

            SkillIcon icon = b.GetComponent<SkillIcon>();
            icon.skillicon = nowselectedUnit.skills[i].skillicon;
            icon.myskill = nowselectedUnit.skills[i];
        }

        for (int i = 0; i < nowselectedUnit.actcount; i++)
        {
            GameObject b = Instantiate(pre_actcounticon, actcounts_transform);
        }

        skillblabla.SetActive(false);
    }

    public void SkillBlaBla(GameObject me)
    {
        skillblabla.SetActive(true);
        if(blablaacs_transform.childCount != 0)
        {
            for (int i = 0; i < blablaacs_transform.childCount; i++)
            {
                Destroy(blablaacs_transform.GetChild(i).gameObject);
            }
        }

        Skill nowskill = me.GetComponent<SkillIcon>().myskill;
        skillname.text = nowskill.skillname;

        if (nowskill.useactcount > 0)
        {
            for (int i = 0; i < nowskill.useactcount; i++)
            {
                Instantiate(pre_actcounticon, blablaacs_transform);
            }
        }

        string youarewhatskill = null;
        switch (nowskill)
        {
            case IDamagedSkill dmg:
                youarewhatskill += $"<color=#E57878>피해</color>: {dmg.mindamage} - {dmg.maxdamage}\n";
                break;
        }

        skillexplanation.text = youarewhatskill + "\n" + nowskill.skillblabla;
    }

    IEnumerator SizeSetAnimation(RectTransform what, Vector2 target, float speed)
    {
        while (what.sizeDelta != target)
        {
            float x = Mathf.Lerp(what.sizeDelta.x, target.x, Time.deltaTime * speed);
            float y = Mathf.Lerp(what.sizeDelta.y, target.y, Time.deltaTime * speed);

            what.sizeDelta = new Vector2(x, y);
            yield return null;
        }
    }

    public void TurnStart()
    {
        OnTurnStarted?.Invoke();
    }
}
