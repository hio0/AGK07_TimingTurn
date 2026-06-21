using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DotDamage : MonoBehaviour
{
    public string dotname;
    public Sprite dotImage;

    public int damage;

    public abstract void Effect(Unit target);
}
