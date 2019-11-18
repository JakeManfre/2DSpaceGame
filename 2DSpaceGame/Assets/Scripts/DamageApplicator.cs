using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageApplicator
{
    public static void ApplyDamage(Damager damager, Damageable damageable)
    {
        if (!damager || !damageable) { return; }
        damageable.TakeDamage(damager.GetDamageDone());
    }

    public static void TryApplyDamage(Damager damager, GameObject shouldBeDamageable)
    {
        Damageable damageable = shouldBeDamageable.GetComponent<Damageable>();
        ApplyDamage(damager, damageable);
    }

    public static void TryApplyDamage(GameObject shouldBeDamager, GameObject shouldBeDamageable)
    {
        Damager damager = shouldBeDamager.GetComponent<Damager>();
        TryApplyDamage(damager, shouldBeDamageable);
    }
}
