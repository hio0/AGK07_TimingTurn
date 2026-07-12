using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float mytiming;
    public Skill myskill;
    public Unit me;
    public Unit targets;


    public void Instalize(float a, Skill b, Unit c, Unit d)
    {
        mytiming = a;
        myskill = b;
        me = c;
        targets = d;
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
