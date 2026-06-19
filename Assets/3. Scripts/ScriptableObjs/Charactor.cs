using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Charactor
{
    public string CharactorName { get; set; }

    public ISkill[] Skills { get; set; }
}
