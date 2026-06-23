using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    EventTrigger trigger;
    public GameObject select;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("클릭됨");
    }

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<EventTrigger>();
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
        }
        else
        {
            return;
        }
    }

    public void OnEnter()
    {
        Debug.Log("Fsf");
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
