using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurOne : MonoBehaviour, ICanAttack, ICanDamaged
{
    public Charactor me;

    public bool stoptimer { get; set; }
    public event Action OnDamaged;


    // Start is called before the first frame update
    void Start()
    {
        FightManager.fight.OnTurnStarted += Damaged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(ICanDamaged target, ISkill skill)
    {
        
    }

    public void Damaged()
    {

    }
}
