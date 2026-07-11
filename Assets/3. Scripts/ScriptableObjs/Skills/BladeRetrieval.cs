using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skils/BladeRetrieval")]
public class BladeRetrieval : Skill, IDamagedSkill
{
    public int Mindamage;
    public int Maxdamage;

    public int mindamage => Mindamage;
    public int maxdamage => Maxdamage;


    public override void Effect(Unit target)
    {
        Attack(mindamage, maxdamage, target);
    }
}
