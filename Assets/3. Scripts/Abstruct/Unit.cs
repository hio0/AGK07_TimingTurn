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

    public event Action OnActedStart; // 공격 시
    public event Action OnActed; // 공격 적중 시
    public event Action OnDamaged; // 피격 시
    public event Action OnDyed; // 사망 시

    // SerizalField private = inspector 상에선 보이게, 대신 캡슐화 / public = 다 가능 / 프로퍼티 = 외부에서 접근 가능, inspector에선 안보임.

    [Header("시스템")]
    public Unit targetedunit;
    public Skill selectedskill;
    public GameObject icanselected;

    void Start()
    {
        FightEvent.OnFightStarted += ResetToDefultValue;
        FightEvent.OnWaitStarted += ResetActCount;

        icanselected.SetActive(false);
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

    public virtual void Act()
    {
        OnActedStart?.Invoke();
        Action act = () =>
        {
            OnActed?.Invoke();
            selectedskill.Effect(targetedunit);
        };
        targetedunit.Damaged(act);
    }

    public virtual void Damaged(Action effect)
    {
        OnDamaged?.Invoke();
        effect?.Invoke();

        if (hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        OnDyed?.Invoke();
        Destroy(gameObject);
    }
}
