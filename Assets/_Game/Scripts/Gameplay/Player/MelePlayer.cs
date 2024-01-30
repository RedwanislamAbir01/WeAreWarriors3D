using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelePlayer : Player
{
    [SerializeField] private PlayerAnimation playerAnimation;


    protected override void DetectTarget()
    {
        base.DetectTarget();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, targetLayer);
        foreach (var hitCollider in hitColliders)
        {
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();
            if (damageable != null && !damageable.IsDead())
            {
                MoveTowardsTarget(hitCollider.transform);
                Attack(damageable.GetTransform());
                return;
            }
           

        }
        MoveTowardsTarget(baseManager.EnemyBase.transform);
        playerAnimation.StopHitAnim();
    }
    public override void Attack(Transform target)
    {
        base.Attack(target);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            playerAnimation.PlayHitAnim(); // Play the attack animation
        }
        
    }
}
