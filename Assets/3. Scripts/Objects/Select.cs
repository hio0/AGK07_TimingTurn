using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    CanvasGroup can;
    [SerializeField] bool isour;

    // Start is called before the first frame update
    void Start()
    {
        can = GetComponent<CanvasGroup>();
        Action tr = () => can.alpha = 1;
        Action fal = () => can.alpha = 0;

        FightManager.fight.OnNextUnit += fal;
        if (isour)
        {
            Action act = () =>
            {
                int mycount = transform.parent.GetComponent<Unit>().nowcount;

                if (FightManager.fight.unitselectednum == mycount)
                {
                    tr();
                }
            };

            FightManager.fight.OnNextUnit += act;
        }
        else
        {
            Enemy enemy = transform.parent.GetComponent<Enemy>();

            enemy.OnEnemyClick += fal;
            enemy.OnEnemyEnter += tr;
            enemy.OnEnemyExit += fal;
        }

        fal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
