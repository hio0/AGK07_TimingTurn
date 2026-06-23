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

    public Dictionary<float, Action> skillList = new Dictionary<float, Action>();
    public float timer;
    public bool ifindtarget;
    public Unit findingunit;
    public Skill findingskill;
    public Unit[] nowtargets;

    bool isstop;

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

    public GameObject timeline;
    public Image timelinefill;
    public Transform arrow_transform;
    public GameObject pre_arrows;
    public GameObject enemy_arrows;
    public GameObject player_arrows;


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
        OnTurnFinished += end;
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

            if(Input.GetKeyDown(KeyCode.F))
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
        StartCoroutine(SizeSetAnimation(garimmack, new Vector2(273.4f, 30f), 2f));

        yield return new WaitForSeconds(2f);
        garimmack.gameObject.SetActive(false);

        CanvasGroup can = startP.GetComponent<CanvasGroup>();
        float second = 2f;

        OnFightStarted?.Invoke();
        StartCoroutine(UIMovement.UIMove.FadeOut(can, second));

        yield return new WaitForSeconds(second + 0.2f);

        StartCoroutine(WaitStart());
    }


    IEnumerator WaitStart()
    {
        OnWaitStarted?.Invoke();
        waitP.SetActive(false);

        StartCoroutine(SizeSetAnimation(upMage, new Vector2(1944, 197.9f), 3.5f));
        StartCoroutine(SizeSetAnimation(downMage, new Vector2(1944, 272.3f), 3.5f));

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

        skillblabla.SetActive(false);
    }

    public void SkillBlaBla(GameObject me)
    {
        skillblabla.SetActive(true);
        if (blablaacs_transform.childCount != 0)
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

        string howtoattack = null;
        switch (nowskill.mytype)
        {
            case Skill.skilltype.closerange:
                howtoattack = "근거리";
                break;
            case Skill.skilltype.longrange:
                howtoattack = "원거리";
                break;
        }
        skilltype.text = howtoattack;

        string youarewhatskill = null;
        switch (nowskill)
        {
            case IDamagedSkill dmg:
                youarewhatskill += $"<color=#E57878>피해</color>: {dmg.mindamage} - {dmg.maxdamage}\n";
                break;
        }

        skillexplanation.text = youarewhatskill + "\n" + nowskill.skillblabla;
    }

    public void TargetFind(Unit actor, Skill skill)
    {
        findingskill = null;
        nowtargets = null;
        findingunit = null;

        ifindtarget = true;

        findingskill = skill;
        findingunit = actor;

        for(int i = 0; i < EnemyRange.childCount; i++)
        {
            EnemyRange.GetChild(i).GetChild(0).GetChild(1).Find("Select").gameObject.SetActive(true);
        }
    }

    public void ActSet(Unit target)
    {
        for (int i = 0; i < EnemyRange.childCount; i++)
        {
            EnemyRange.GetChild(i).GetChild(0).GetChild(1).Find("Select").gameObject.SetActive(false);
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

            findingunit.TryGetComponent<ICanAttack>(out ICanAttack dam);
            dam.Attack();
        };

        float timing = (float)Math.Round(findingskill.timing, 1);

        skillList.Add(timing, actready);
        float posx = 0;
        switch(timing)
        {
            case 0.5f:
                posx = -401.2f;
                break;
            case 1.5f:
                posx = 0;
                break;
        }

        bool isok = true;
        GameObject b = null;
        if(arrow_transform.childCount != 0)
        {
            for(int i = 0; i < arrow_transform.childCount; i++)
            {
                for(int j = 0; j < arrow_transform.GetChild(i).childCount; j++)
                {
                    if(arrow_transform.GetChild(i).GetChild(j).GetComponent<Arrow>().mytiming == timing)
                    {
                        b = arrow_transform.GetChild(i).GetChild(j).gameObject;
                        isok = false;
                        break;
                    }
                }
            }
        }

        if(isok)
        {
            b = Instantiate(pre_arrows, arrow_transform);
            b.GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, -82.3f);
        }
        Transform a = b.transform;
        GameObject arr = Instantiate(arrow, a);

        arr.TryGetComponent<Arrow>(out Arrow r);
        r.mytiming = timing;
        r.myskill = findingskill;
        r.me = findingunit;
        r.targets = target;

        ifindtarget = false;
        NowSelectUnit(unitselectednum - 1);
    }

    IEnumerator SizeSetAnimation(RectTransform what, Vector2 target, float speed)
    {
        while ((what.sizeDelta - target).sqrMagnitude > 0.001f)
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
        isstop = false;

        StartCoroutine(Act());
    }

    IEnumerator Act()
    {
        float mftimer = 0f;

        timer = 0;
        timelinefill.gameObject.SetActive(true);
        timelinefill.fillAmount = 0;

        while (timer != 3)
        {
            if (!isstop)
            {
                timer += Time.deltaTime;
                mftimer = (float)Math.Round(timer, 1);
                timelinefill.fillAmount = timer / 3;


                foreach (KeyValuePair<float, Action> list in skillList)
                {
                    if (list.Key == mftimer)
                    {
                        list.Value?.Invoke();
                    }
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(TurnFinish());
    }

    IEnumerator TurnFinish()
    {
        waitP.SetActive(false);
        timeline.SetActive(false);

        OnTurnFinished?.Invoke();
        StartCoroutine(SizeSetAnimation(upMage, new Vector2(1944, 0f), 7f));
        yield return StartCoroutine(SizeSetAnimation(downMage, new Vector2(1944, 0f), 7f)); ;

        StartCoroutine(WaitStart());
    }
}
