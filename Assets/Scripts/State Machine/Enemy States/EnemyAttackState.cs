using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAttackState : EnemyBaseState
{
    float elapsedTime;

    public EnemyAttackState(EnemySM stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.navAgent.isStopped = true;
        stateMachine.Animator.CrossFadeInFixedTime("Idle", .2f);
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.playerTransform == null) return;

        if (Vector3.Distance(stateMachine.transform.position, stateMachine.playerTransform.position) > stateMachine.AttackDistance)
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }

        RotateTowardsPlayer();


        elapsedTime += deltaTime;
        if (elapsedTime >= stateMachine.AttackInterval)
        {
            Debug.Log("Attack");
            stateMachine.AttackPlayer();
            elapsedTime = 0;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = stateMachine.playerTransform.position - stateMachine.transform.position;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}
