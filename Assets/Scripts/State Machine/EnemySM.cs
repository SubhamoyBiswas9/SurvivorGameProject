using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySM : StateMachine, IDeathHandler, IHitHandler
{
    [SerializeField] float moveSpeed = 3f;
    [field: SerializeField] public float AttackInterval { get; private set; } = .5f;
    [field: SerializeField] public float AttackDistance { get; private set; } = .2f;
    [field: SerializeField] public int Damage { get; private set; } = 10;

    [SerializeField] GameObject coinPrefab;

    public NavMeshAgent navAgent { get; private set; }

    public Animator Animator { get; private set; }

    public Health Health { get; private set; }

    public Transform playerTransform {  get; private set; }

    public static int count;

    public static event Action<Health> OnHitEvent;

    private void Awake()
    {
        count++;
    }

    public void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        navAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Health = GetComponent<Health>();

        SwitchState(new EnemyChaseState(this));
    }

    public virtual void AttackPlayer()
    {
        playerTransform?.GetComponent<IDamageable>()?.Damage(Damage);
        Debug.Log("turtle attacking");
        Animator.CrossFadeInFixedTime("Attack", .2f);
    }

    private void OnDestroy()
    {
        count--;
    }

    public void OnDeath()
    {
        ScoreManager.Instance.OnEnemyKilled();
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnHit()
    {
        SoundManager.Instance.EnemyHitSFX();
        OnHitEvent?.Invoke(Health);
    }
}
