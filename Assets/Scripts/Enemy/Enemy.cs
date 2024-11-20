using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    [SerializeField] float attackInterval = .5f;
    [SerializeField] float attackDistance = .2f;

    Transform playerTransform;

    NavMeshAgent agent;

    public static int count;

    private void Awake()
    {
        count++;
    }

    public void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(playerTransform.position);

        if (Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
        {
            playerTransform.GetComponent<IDamageable>().Damage(10);
        }
    }

    private void OnDestroy()
    {
        count--;
    }
}
