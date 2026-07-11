using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : DotDamage
{
    public override void Effect(Unit target)
    {
        Action effect = () =>
        {
            
        };

        FightEvent.OnTurnStarted += effect;
    }
}
