using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanAttack
{
    IEnumerator Attack(ICanDamaged target, ISkill skill);
}
