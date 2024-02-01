using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : Enemy
{
    [SerializeField] private EnemyAnimation enemyAnimation;


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
        MoveTowardsTarget(baseManager.PlayerBase.transform);
        enemyAnimation.StopHitAnim();
    }
    public override void Attack(Transform target)
    {
        base.Attack(target);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            enemyAnimation.PlayHitAnim(); // Play the attack animation
        }

    }
}