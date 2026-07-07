using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Unit targetedunit;
    public Skill selectedskill;

    void Start()
    {
        FightManager.fight.OnFightStarted += ResetToDefultValue;
        FightManager.fight.OnWaitStarted += ResetActCount;
    }

    void Update()
    {
        if(hp <= 0)
        {
            Die();
        }
    }

    void OnValidate()
    {
        Image img = gameObject.GetComponent<Image>();
        img.SetNativeSize();
    }

    public void ResetToDefultValue()
    {
        hp = unitdata.defulthp;
        stamina = unitdata.defultstamina;
        actcount = unitdata.defultactcount;
        dodge_persent = unitdata.defultdodge_persent;
    }

    void ResetActCount()
    {
        actcount = unitdata.defultactcount;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // 템플릿들
    public void DefultHit(ICanDamaged target, int damage)
    {

    }


    public void DefultAttack(Unit target, Action effect)
    {
        target.gameObject.TryGetComponent<ICanDamaged>(out ICanDamaged dam);
        dam.OnDamaged += effect;
        dam.Damaged();
    }
}
