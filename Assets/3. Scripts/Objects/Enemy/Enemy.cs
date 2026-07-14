using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public event Action OnEnemyEnter;
    public event Action OnEnemyClick;
    public event Action OnEnemyExit;

    // Start is called before the first frame update
    void Start()
    {
        FightEvent.OnWaitStarted += ActSet;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void ActSet()
    {
        Transform ourrange = GameObject.Find("OurRange").transform;
        int r = UnityEngine.Random.Range(0, ourrange.childCount);
        Unit unit = ourrange.GetChild(r).GetChild(0).GetComponent<Unit>();

        Unit me = gameObject.GetComponent<Unit>();

        int l = UnityEngine.Random.Range(0, me.skills.Length);
       
        FightManager.fight.findingunit = me;
        FightManager.fight.findingskill = me.skills[l];
        FightManager.fight.ActSet(unit);
    }

    public virtual void OnClick()
    {
        if (FightManager.fight.ifindtarget)
        {
            FightManager.fight.ActSet(gameObject.GetComponent<Unit>());
            OnEnemyClick?.Invoke();
        }
        else
        {
            return;
        }
    }

    public virtual void OnEnter()
    {
        if (FightManager.fight.ifindtarget)
        {
            OnEnemyEnter?.Invoke();
        }
    }

    public virtual void OnExit()
    {
        if (FightManager.fight.ifindtarget)
        {
            OnEnemyExit?.Invoke();
        }
    }
}
