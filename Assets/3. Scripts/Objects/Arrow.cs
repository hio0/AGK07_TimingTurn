using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float mytiming;
    public Skill myskill;
    public Unit me;
    public Unit targets;

    public GameObject pre_arrowinskill;
    GameObject b;

    public void Instalize(float a, Skill b, Unit c, Unit d)
    {
        mytiming = a;
        myskill = b;
        me = c;
        targets = d;
    }

    // Start is called before the first frame update
    void Start()
    {
        Action act = () => Destroy(gameObject);
        me.OnDyed += act;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ArrowInSkill()
    {
        b = Instantiate(pre_arrowinskill, FightManager.fight.textP.transform);

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(b.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out localPos);
        
        b.GetComponent<RectTransform>().anchoredPosition = new Vector2(localPos.x + 80f, localPos.y - 70f);
        b.GetComponent<ArrowInSkill>().Instalize(mytiming, myskill, me, targets);
    }

    public void ARISKEnd()
    {
        Destroy(b);
    }
}
