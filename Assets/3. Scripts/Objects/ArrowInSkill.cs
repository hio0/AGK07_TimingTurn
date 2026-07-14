using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowInSkill : MonoBehaviour
{
    public TMP_Text timingT;
    public TMP_Text skillT;
    public TMP_Text meT;
    public TMP_Text targetT;

    public void Instalize(float timing, Skill skill, Unit me, Unit target)
    {
        timingT.text = timing.ToString() + "s";
        skillT.text = skill.skillname;
        meT.text = me.unitdata.charactorName;
        targetT.text = target.unitdata.charactorName;
    }

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
