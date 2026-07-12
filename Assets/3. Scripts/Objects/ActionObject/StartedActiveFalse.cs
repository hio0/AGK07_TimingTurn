using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartedActiveFalse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FightEvent.OnFightStarted += Fal;
        FightEvent.OnWaitStarted += Tru;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fal()
    {
        gameObject.SetActive(false);
    }

    void Tru()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        FightEvent.OnFightStarted -= Fal;
        FightEvent.OnWaitStarted -= Tru;
    }
}
