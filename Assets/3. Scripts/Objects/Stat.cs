using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    Unit myunit;
    int hp;
    int stamina;

    public Image hpbar;
    public TMP_Text hpT;

    public Image staminabar;

    // Start is called before the first frame update
    void Start()
    {
        myunit = gameObject.GetComponentInParent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        hp = myunit.hp;
        stamina = myunit.stamina;

        hpbar.fillAmount = hp / myunit.unitdata.defulthp;
        hpT.text = $"{hp}/{myunit.unitdata.defulthp}";

        staminabar.fillAmount = stamina / myunit.unitdata.defultstamina;
    }
}
