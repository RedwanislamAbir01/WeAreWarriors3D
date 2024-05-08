using DG.Tweening;

using UnityEngine;

public class MelePlayer : Player
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField]
    private GameObject _renderer;

    private float originalYPosition;
    private bool isJumping = false;
    private Tweener jumpTween;
    protected override void Init()
    {
        base.Init();
        originalYPosition = _renderer.transform.localPosition.y;
    }
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

    protected override void CharacterActivity()
    {
        base.CharacterActivity();
        if (agent.velocity.magnitude > 0) // If character is moving
        {
            if (!isJumping)
            {
                isJumping = true;
                jumpTween = _renderer.transform.DOLocalMoveY(originalYPosition + 0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        else
        {
            if (isJumping)
            {
                isJumping = false;
                jumpTween.Kill(); // Stop the jump tween if character stops moving
                _renderer.transform.localPosition = new Vector3(_renderer.transform.localPosition.x, originalYPosition, _renderer.transform.localPosition.z);
            }
        }
    }
}
