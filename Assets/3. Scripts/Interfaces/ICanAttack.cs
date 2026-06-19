using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanAttack
{
    bool stoptimer { get; set; }

    void Attack(ICanDamaged target, ISkill skill);
}
