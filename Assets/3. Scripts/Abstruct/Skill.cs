using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Skill : ScriptableObject
{
    public string skillname;
    public Sprite skillicon;
    public int useactcount;
    [TextArea]
    public string skillblabla;
    public float timing;

    public enum skilltype
    {
        closerange,
        longrange
    }
    public skilltype mytype;

    public abstract void Effect(Unit target);

    //템플릿
    protected void Attack(int mindamage, int maxdamage, Unit target) // 자식만 접근 가능한 protected
    {
        int damage = Random.Range(mindamage, maxdamage + 1);

        target.hp -= damage;
        FightManager.fight.HittedReaction(damage, target);
    }
}
