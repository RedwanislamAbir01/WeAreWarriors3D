using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseCharacter 
{


    [SerializeField] private protected float movememntSpeed = .3f;
    [SerializeField] private protected LayerMask targetLayer;
    [SerializeField] private protected float attackRadius = 2f; // Adjust as needed

    protected BaseManager baseManager;
    protected NavMeshAgent agent;

    private EnemyHeath enemyHealth;
    private void Start()
    {
        baseManager = BaseManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movememntSpeed;
        enemyHealth = GetComponent<EnemyHeath>();   
    }

    private void Update()
    {
        DetectTarget();
    }

    protected virtual void DetectTarget()
    {

    }

    public override void MoveTowardsTarget(Transform target)
    {
        if(!enemyHealth.IsDead())
        agent.SetDestination(target.position);
    }

    public override void Attack(Transform target)
    {

    }
}
