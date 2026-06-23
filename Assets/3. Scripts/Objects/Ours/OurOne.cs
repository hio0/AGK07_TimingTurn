using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurOne : Unit, ICanAttack, ICanDamaged
{
    public event Action OnDamaged;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Action act = () => selectedskill.Effect(targetedunit);

        DefultAttack(targetedunit, act);
    }

    public void Damaged()
    {
        OnDamaged?.Invoke();
    }
}
