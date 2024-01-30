using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{ 
[SerializeField] private Animator _animator;

private static readonly int Hit = Animator.StringToHash("CanHit");

private static readonly int Dead = Animator.StringToHash("Death");
public void PlayHitAnim() => _animator.SetBool(Hit, true);

public void StopHitAnim() => _animator.SetBool(Hit, false);

public void PlayDeathAnim() => _animator.SetTrigger(Dead);
}
