using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OurTwo : Unit, ICanAttack, ICanDamaged
{
    public event Action OnDamaged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(Unit target)
    {
        Action act = () => selectedskill.Effect(target);

        target.gameObject.TryGetComponent<ICanDamaged>(out ICanDamaged dam);
        dam.OnDamaged += act;
    }

    public void Damaged()
    {
        OnDamaged?.Invoke();
    }
}
