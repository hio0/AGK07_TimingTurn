using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DOTween
{
    public static void DOShaking(Transform what, float strength, int vibrato, int randomess)
    {
        what.DOShakePosition(0.5f, strength, vibrato, randomess);
    }
}
