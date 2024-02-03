using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armature : Weapon
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(18);
            SoundManager.Instance.PlaySound(SoundManager.Instance._audioClipRefsSO.hitSound);
        }
    }
}