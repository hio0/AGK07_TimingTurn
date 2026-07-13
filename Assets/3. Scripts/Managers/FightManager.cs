using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class FightManager : MonoBehaviour
{
    [Header("시스템")]
    public static FightManager fight;

    public Transform OurRange;
    public Transform EnemyRange;

    public Dictionary<float, Action> skillList = new Dictionary<float, Action>();
    public float timer;
    public bool ifindtarget;
    public Unit findingunit;
    public Skill findingskill;
    public Unit[] nowtargets;

    bool isstop;

    [Header("UI")]
    public RectTransform upMage;
    public RectTransform downMage;

    public GameObject startP;
    public GameObject waitP;
    public GameObject turnP;
    public Transform textP;

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

    public event Action OnSet_Skillblabla;
    public event Action OnNonSet_Skillblabla;
    public event Action OnSkillSet;
    public event Action OnTargetFinding;

    public GameObject timeline;
    public Image timelinefill;
    public Transform arrow_transform;
    public GameObject pre_arrows;
    public GameObject enemy_arrows;
    public GameObject player_arrows;
    public GameObject turnstartB;

    public GameObject pre_damageT;


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
        StartCoroutine(StartStart());

        Action end = () =>
        {
            turncount++;
        };
        FightEvent.OnTurnFinished += end;
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

            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(TurnFinish());
            }
        }
    }

    IEnumerator StartStart()
    {
        startP.SetActive(true);
        waitP.SetActive(false);

        turncount = 1;
        timeline.SetActive(false);
        timelinefill.gameObject.SetActive(false);

        upMage.sizeDelta = new Vector2(1944f, 0f);
        downMage.sizeDelta = new Vector2(1944f, 0f);

        garimmack.sizeDelta = new Vector2(273.4f, 66.94f);
        garimmack.gameObject.SetActive(true);
        StartCoroutine(UIMovement.SizeSetAnimation(garimmack, new Vector2(273.4f, 30f), 2f));

        yield return new WaitForSeconds(2f);
        garimmack.gameObject.SetActive(false);

        CanvasGroup can = startP.GetComponent<CanvasGroup>();
        float second = 2f;

        FightEvent.OnFightStarted?.Invoke();
        StartCoroutine(UIMovement.FadeOut(can, second));

        yield return new WaitForSeconds(second + 0.2f);

        StartCoroutine(WaitStart());
    }


    IEnumerator WaitStart()
    {
        FightEvent.OnWaitStarted?.Invoke();
        waitP.SetActive(false);
        turnstartB.SetActive(false);

        StartCoroutine(UIMovement.SizeSetAnimation(upMage, new Vector2(1944, 197.9f), 3.5f));
        StartCoroutine(UIMovement.SizeSetAnimation(downMage, new Vector2(1944, 272.3f), 3.5f));

        yield return new WaitForSeconds(1.5f);

        waitP.SetActive(true);
        timeline.SetActive(true);
        turnT.text = $"<size=60><b>{turncount}</b></size>Turn";

        unitselectednum = OurRange.childCount - 1; // 항상 맨 앞의 놈부터 보여줄것
        NowSelectUnit(OurRange.childCount - 1);
    }

    public void NowSelectUnit(int now)
    {
        OurRange.GetChild(unitselectednum).gameObject.transform.GetChild(0).Find("Select").gameObject.SetActive(false);

        unitselectednum = now;
        if (unitselectednum > OurRange.childCount)
        {
            unitselectednum = OurRange.childCount - 1;
        }
        else if (unitselectednum < 0)
        {
            unitselectednum = 0;
        }
        GameObject b = OurRange.GetChild(unitselectednum).gameObject.transform.GetChild(0).gameObject;
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
            icon.mymy = nowselectedUnit;
        }

        for (int i = 0; i < nowselectedUnit.actcount; i++)
        {
            GameObject b = Instantiate(pre_actcounticon, actcounts_transform);
        }

        OnNonSet_Skillblabla?.Invoke();
    }

    public void SkillBlaBla(GameObject me)
    {
        Skill nowskill = me.GetComponent<SkillIcon>().myskill;

        skillblabla.GetComponent<SkillInfo>().Installize(nowskill);
        OnSet_Skillblabla?.Invoke();
    }

    public void TargetFind(Unit actor, Skill skill)
    {
        findingskill = null;
        nowtargets = null;
        findingunit = null;

        ifindtarget = true;

        findingskill = skill;
        findingunit = actor;

        OnTargetFinding?.Invoke();
    }

    public void ActSet(Unit target)
    {
        OnSkillSet?.Invoke();

        if (findingskill.useactcount > findingunit.actcount)
        {
            return;
        }

        GameObject arrow = enemy_arrows;
        if (findingunit.transform.parent.parent.name == "OurRange")
        {
            arrow = player_arrows;
        }

        Action actready = () =>
        {
            findingunit.selectedskill = findingskill;
            findingunit.targetedunit = target;

            findingunit.Act();
        };

        float timing = (float)Math.Round(findingskill.timing, 1); // 한자릿수까지

        if (skillList.ContainsKey(timing))
        {
            skillList[timing] += actready;
        }
        else
        {
            skillList.Add(timing, actready);
        }
        float posx = 0;
        switch (timing)
        {
            case 0.5f:
                posx = -401.2f;
                break;
            case 1.5f:
                posx = 0;
                break;
        }

        findingunit.actcount -= findingskill.useactcount;
        for (int i = 0; i < actcounts_transform.childCount; i++)
        {
            Destroy(actcounts_transform.GetChild(i).gameObject);
        }

        bool isok = true;
        GameObject b = null;
        if (arrow_transform.childCount != 0)
        {
            for (int i = 0; i < arrow_transform.childCount; i++)
            {
                for (int j = 0; j < arrow_transform.GetChild(i).childCount; j++)
                {
                    if (arrow_transform.GetChild(i).GetChild(j).GetComponent<Arrow>().mytiming == timing)
                    {
                        b = arrow_transform.GetChild(i).gameObject;
                        isok = false;
                        break;
                    }
                }
            }
        }

        if (isok)
        {
            b = Instantiate(pre_arrows, arrow_transform);
            b.GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, -82.3f);
        }
        GameObject arr = Instantiate(arrow, b.transform);

        Arrow r = arr.GetComponent<Arrow>();
        r.Instalize(timing, findingskill, findingunit, target);

        ifindtarget = false;

        int a = unitselectednum - 1;
        if (a < 0)
        {
            turnstartB.SetActive(true);
        }
        NowSelectUnit(a);
    }

    public void TurnStart()
    {
        FightEvent.OnTurnStarted?.Invoke();
        isstop = false;
        waitP.SetActive(false);

        StartCoroutine(Act());
    }

    IEnumerator Act()
    {
        float mftimer = 0f;

        timer = 0;
        timelinefill.gameObject.SetActive(true);
        timelinefill.fillAmount = 0;

        while (timer < 3)
        {
            if (!isstop)
            {
                timer += Time.deltaTime;
                mftimer = (float)Math.Round(timer, 1);
                timelinefill.fillAmount = timer / 3f;

                if (skillList.TryGetValue(mftimer, out Action action))
                {
                    action?.Invoke();
                    isstop = true;

                    if (action.GetInvocationList().Length > 1) // 2인 이상 동시 행동
                    {

                    }
                    else
                    {
                        turnP.SetActive(true);
                    }

                    yield return new WaitForSeconds(2f);
                    isstop = false;
                    turnP.SetActive(false);

                    skillList.Remove(mftimer);
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(TurnFinish());
    }

    public void HittedReaction(int damage, Unit target)
    {
        RectTransform rec = target.GetComponent<RectTransform>();

        GameObject b = Instantiate(pre_damageT, textP, false);
        b.GetComponent<DamagedText>().Instalize(damage, true);

        RectTransform brect = b.GetComponent<RectTransform>();
        RectTransform bP = brect.parent as RectTransform;

        // target의 화면 좌표
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, rec.position);

        // damage의 부모 기준 로컬 좌표로 변환
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(bP, screenPos, Camera.main, out localPos);

        // 적용
        brect.anchoredPosition = localPos;
    }

    IEnumerator TurnFinish()
    {
        waitP.SetActive(false);
        turnP.SetActive(false);
        timeline.SetActive(false);
        timelinefill.gameObject.SetActive(false);
        for (int i = 0; i < arrow_transform.childCount; i++)
        {
            Destroy(arrow_transform.GetChild(i).gameObject);
        }
        skillList.Clear();

        FightEvent.OnTurnFinished?.Invoke();
        StartCoroutine(UIMovement.SizeSetAnimation(upMage, new Vector2(1944, 0f), 7f));
        yield return StartCoroutine(UIMovement.SizeSetAnimation(downMage, new Vector2(1944, 0f), 7f)); ;

        StartCoroutine(WaitStart());
    }
}
