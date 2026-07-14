using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    CanvasGroup can;

    void Start()
    {
        can = GetComponent<CanvasGroup>();
        Action act = () =>
        {
            can.alpha = 1f;
            DOTween.DOFade(can, 0, 1f);
        };
        FightEvent.OnDamaged += act;

        can.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
