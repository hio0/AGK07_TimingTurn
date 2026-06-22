using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillname;
    public Sprite skillicon;
    public int useactcount;
    [TextArea]
    public string skillblabla;
    public float timing;

    public enum skilltype
    {
        closerange,
        longrange
    }
    public skilltype mytype;

    public abstract void Effect(Unit target);

    // 템플릿
    public void DefultGiveEffect()
    {

    }
}
