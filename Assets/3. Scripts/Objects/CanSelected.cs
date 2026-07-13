using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSelected : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Action tr = () => gameObject.SetActive(true);
        Action fa = () => gameObject.SetActive(false);

        FightManager.fight.OnTargetFinding += tr;
        FightManager.fight.OnSkillSet += fa;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
