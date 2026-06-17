using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    string skillname { get; set; }

    void Effet();
}
