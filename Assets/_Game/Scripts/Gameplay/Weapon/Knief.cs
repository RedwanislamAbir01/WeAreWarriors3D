using UnityEngine;

public class Knife : Weapon
{
    private void OnTriggerEnter(Collider other)
    {
        // Try to get the IDamageable component
        if (other.TryGetComponent(out IDamageable damageable))
        {
            // If it implements IDamageable, apply damage
            damageable.TakeDamage(damageAmount);
            SoundManager.Instance.PlaySound(SoundManager.Instance._audioClipRefsSO.hitSound);
        }
    }

}