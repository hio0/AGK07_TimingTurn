using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurOne : Unit, ICanAttack, ICanDamaged
{
    public event Action OnDamaged;

    // Start is called before the first frame update
    void Start()
    {
        //FightManager.fight.OnTurnStarted += Damaged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(Unit target)
    {
        Action act = () => selectedskill.Effect(target);

        DefultAttack(target, act);
    }

    public void Damaged()
    {
        OnDamaged?.Invoke();
    }
}
