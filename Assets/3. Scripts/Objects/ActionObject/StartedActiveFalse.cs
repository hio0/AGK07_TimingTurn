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
        FightEvent.OnFightStarted += fals;

        Action tr = () => gameObject.SetActive(true);
        FightEvent.OnWaitStarted += tr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
