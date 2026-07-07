using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OurTwo : Unit, ICanAttack, ICanDamaged
{
    public event Action OnDamaged;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Action act = null;
        act = () => selectedskill.Effect(targetedunit);
        Debug.Log(charactorName);

        DefultAttack(targetedunit, act);
    }

    public void Damaged()
    {
        OnDamaged?.Invoke();
    }
}
