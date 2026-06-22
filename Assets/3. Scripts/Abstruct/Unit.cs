using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("기본 정보")]
    public DefultUnitData unitdata;
    public string charactorName;
    public Skill[] skills;

    public int hp { get; set; }
    public int stamina { get; set; }

    public int actcount { get; set; }
    public float dodge_persent { get; set; }

    [Header("시스템")]
    public Unit[] targetedunit;
    public Skill selectedskill;


    void Start()
    {
        FightManager.fight.OnFightStarted += ResetToDefultValue;
    }

    void ResetToDefultValue()
    {
        hp = unitdata.defulthp;
        stamina = unitdata.defultstamina;
        actcount = unitdata.defultactcount;
        dodge_persent = unitdata.defultdodge_persent;
    }

    // 템플릿들
    public void DefultHit(ICanDamaged target, int damage)
    {

    }

    public void DefultAttack(Unit target, Action effect)
    {
        target.gameObject.TryGetComponent<ICanDamaged>(out ICanDamaged dam);
        float r = UnityEngine.Random.Range(1f, 100f);

        if(r > target.dodge_persent)
        {
            dam.OnDamaged += effect;
        }
        else
        {
            Debug.Log("회피");
            return;
        }
    }
}
