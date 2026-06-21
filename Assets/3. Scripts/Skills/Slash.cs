using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skils/Slash")]
public class Slash : Skill, IDamagedSkill
{
    public int Mindamage;
    public int Maxdamage;

    public int mindamage => Mindamage; // 인터페이스는 값 대신 표시 용도로 사용, inspector에서 뎀지 설정 가능하게 함.
    public int maxdamage => Maxdamage;

    public override void Effect(Unit target)
    {
        int damage = UnityEngine.Random.Range(mindamage, maxdamage + 1);

        target.gameObject.TryGetComponent<ICanDamaged>(out ICanDamaged dam);
        Action act = () => target.hp -= damage;

        dam.OnDamaged += act;
    }
}
