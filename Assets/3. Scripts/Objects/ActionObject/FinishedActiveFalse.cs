using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedActiveFalse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Action act = () => gameObject.SetActive(false);

        FightManager.fight.OnTurnFinished += act;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
