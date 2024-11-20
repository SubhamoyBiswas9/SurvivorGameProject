using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemySM stateMachine;

    public EnemyBaseState(EnemySM stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
