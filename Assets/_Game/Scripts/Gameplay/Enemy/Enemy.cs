using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseCharacter 
{

    [SerializeField] private NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    public override void MoveTowardsTarget(Transform target)
    {
      
    }

    
    public override void Attack(Transform target)
    {
       
    }
}