using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    public Skill nowskill;

    public TMP_Text skillname;
    public TMP_Text skilltype;
    public TMP_Text skillexplanation;

    public GameObject pre_actcounticon;
    public Transform blablaacs_transform;

    public void Installize(Skill skill)
    {
        nowskill = skill;
    }

    // Start is called before the first frame update
    void Start()
    {
        FightManager.fight.OnSet_Skillblabla += Setting;
        FightManager.fight.OnNonSet_Skillblabla += Resetting;

        gameObject.SetActive(false);
    }

    void Setting()
    {
        gameObject.SetActive(true);

        if (blablaacs_transform.childCount != 0)
        {
            for (int i = 0; i < blablaacs_transform.childCount; i++)
            {
                Destroy(blablaacs_transform.GetChild(i).gameObject);
            }
        }

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

        float timing = (float)Math.Round(nowskill.timing, 1);
        skillexplanation.text = $"<color=#F1BF3D><b>{timing}s</b></color>\n" + youarewhatskill + "\n" + nowskill.skillblabla;
    }

    void Resetting()
    {
        gameObject.SetActive(false);
    }
}
