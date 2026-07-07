using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject select;

    // Start is called before the first frame update
    void Start()
    {
        select.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (FightManager.fight.ifindtarget)
        {
            FightManager.fight.ActSet(gameObject.GetComponent<Unit>());
            select.SetActive(false);
        }
        else
        {
            return;
        }
    }

    public void OnEnter()
    {
        if (FightManager.fight.ifindtarget)
        {
            select.SetActive(true);
        }
    }

    public void OnExit()
    {
        if (FightManager.fight.ifindtarget)
        {
            select.SetActive(false);
        }
    }
}
