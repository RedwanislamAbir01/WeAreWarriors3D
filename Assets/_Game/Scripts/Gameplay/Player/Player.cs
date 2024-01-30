using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : BaseCharacter
{
    [SerializeField] private protected float movememntSpeed = .3f;
    [SerializeField] private protected LayerMask targetLayer;
    [SerializeField] private protected float attackRadius = 2f; // Adjust as needed

    protected BaseManager baseManager;
    protected NavMeshAgent agent;
    private void Awake()
    {
        baseManager = BaseManager.Instance;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
      agent.speed = movememntSpeed; 
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
        agent.SetDestination(target.position);
    }

    public override void Attack(Transform target)
    {
  
    }
}
