using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : DotDamage
{
    public override void Effect(Unit target)
    {
        target.gameObject.TryGetComponent<ICanDamaged>(out ICanDamaged dm);

        Action effect = () =>
        {
            Action dama = () => target.hp -= damage;
            dm.OnDamaged += dama;
        };

        FightManager.fight.OnTurnStarted += effect;
    }
}
