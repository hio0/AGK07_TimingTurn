using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("기본 정보")]
    public string charactorName;
    public Skill[] skills;

    [Header("스탯")]
    public int hp;
    public int stamina;

    public int actcount;
    public float dodge_persent;

    [Header("시스템")]
    public Unit[] targetedunit;
    public Skill selectedskill;


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
