using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Phase_Ready : VMyState<PhaseState>
{
    public override PhaseState StateEnum => PhaseState.Phase_Ready;

    protected override void EnterState()
    {
        StartCoroutine(GoToNextState());
    }

    IEnumerator GoToNextState()
    {
        yield return new WaitForSeconds(2.0f);
        OwnerStateMachine.ChangeState(PhaseState.Phase_Running);
    }

    protected  override void ExcuteState()
    {
    }

    protected  override void ExitState()
    {
    }
}
