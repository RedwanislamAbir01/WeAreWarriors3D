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

    private PlayerHealth playerHealth;
    private void Awake()
    {
        baseManager = BaseManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
     Init();    
    }
    protected virtual void Init()
    {
        agent.speed = movememntSpeed;
    }
    private void Update()
    {
        DetectTarget();
        CharacterActivity();
    }
    protected virtual void CharacterActivity()
    {

    }
    protected virtual void DetectTarget()
    {

    }
 
    public override void MoveTowardsTarget(Transform target)
    {
        if (!playerHealth.IsDead())
        {
            agent.SetDestination(target.position);
        }
    }

    public override void Attack(Transform target)
    {
  
    }
}
