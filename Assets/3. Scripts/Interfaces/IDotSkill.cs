using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDotSkill
{
    DotDamage[] givedots {  get; }

    int[] dotcount { get; }
}
