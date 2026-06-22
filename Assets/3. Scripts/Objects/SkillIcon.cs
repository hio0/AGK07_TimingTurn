using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    public Sprite skillicon;
    public Skill myskill;
    public Unit mymy;
    bool isclick;

    EventTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = skillicon;
        trigger = GetComponent<EventTrigger>();

        AddEvent(trigger, EventTriggerType.PointerClick, OnClick);
        AddEvent(trigger, EventTriggerType.PointerEnter, OnEnter);
        AddEvent(trigger, EventTriggerType.PointerExit, OnExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddEvent(EventTrigger trigger, EventTriggerType type, Action<PointerEventData> action)
    {
        if (trigger.triggers == null)
        {
            trigger.triggers = new List<EventTrigger.Entry>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;

        entry.callback.AddListener((data) =>
        {
            action((PointerEventData)data);
        });

        trigger.triggers.Add(entry);
    }

    void OnClick(PointerEventData data)
    {
        FightManager.fight.SkillBlaBla(gameObject);
        isclick = true;
        FightManager.fight.TargetFind(mymy, myskill);
    }

    void OnEnter(PointerEventData data)
    {
        FightManager.fight.SkillBlaBla(gameObject);
    }

    void OnExit(PointerEventData data)
    {
        if(!isclick)
        {
            FightManager.fight.skillblabla.SetActive(false);
        }
    }
}
