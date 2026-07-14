using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSelected : MonoBehaviour
{
    CanvasGroup can;

    void Start()
    {
        can = GetComponent<CanvasGroup>();
        can.alpha = 0;

        Action tr = () => can.alpha = 1f;
        FightManager.fight.OnTargetFinding += tr;

        Action fa = () => can.alpha = 0f;
        FightManager.fight.OnSkillSet += fa;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
