using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    Unit myunit;
    public Image hpbar;
    public TMP_Text hpT;

    public Image staminabar;

    // Start is called before the first frame update
    void Start()
    {
        myunit = transform.parent.GetChild(0).GetComponentInParent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.fillAmount = myunit.hp / (float)myunit.unitdata.defulthp;
        hpT.text = $"{myunit.hp}/{myunit.unitdata.defulthp}";

        staminabar.fillAmount = myunit.stamina / myunit.unitdata.defultstamina;
    }
}
