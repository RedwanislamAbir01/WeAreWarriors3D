using System;
using UnityEngine;

public interface IDamageable
{
    public event Action OnDeath;
    void TakeDamage(int amount);
    bool IsDead();
    Transform GetTransform();


}
