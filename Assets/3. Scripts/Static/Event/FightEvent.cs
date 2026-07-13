using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FightEvent // fight 씬에서만 사용되는 스크립튼데, 그럼 정적 class가 아니라 싱글톤으로 참조할 수 있게 하는게. 근데 귀찮아서...
{
    public static Action OnFightStarted; // 전투 돌입 시
    public static Action OnWaitStarted; // 명령 페이즈 시작
    public static Action OnTurnStarted; // 턴 시작 시
    public static Action OnTurnFinished; // 턴 끝날 시

    public static Action OnDamaged; // 퍄해 입을 때

    public static void ClearEvent()
    {
        OnFightStarted = null;
        OnWaitStarted = null;
        OnTurnStarted = null;
        OnTurnFinished = null;
        OnDamaged = null;
    }
}
