using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartedActiveFalse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Action fals = () => gameObject.SetActive(false);
        FightManager.fight.OnFightStarted += fals;

        Action tr = () => gameObject.SetActive(true);
        FightManager.fight.OnWaitStarted += tr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
