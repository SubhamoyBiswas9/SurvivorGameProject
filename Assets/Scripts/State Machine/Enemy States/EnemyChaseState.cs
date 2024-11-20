using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemySM stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.navAgent.isStopped = false;
        stateMachine.Animator.CrossFadeInFixedTime("Chase", .3f);
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.playerTransform == null) return;

        if (Vector3.Distance(stateMachine.transform.position, stateMachine.playerTransform.position) < stateMachine.AttackDistance)
        {
            stateMachine.SwitchState(new EnemyAttackState(stateMachine));
        }
        else
            stateMachine.navAgent.SetDestination(stateMachine.playerTransform.position);

    }
}
