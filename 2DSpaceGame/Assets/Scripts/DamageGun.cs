using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGun : Gun
{
    protected override void Projectile_OnTriggerEnter2D(Projectile projectile, Collider2D collision)
    {
        DamageApplicator.TryApplyDamage(projectile.gameObject, collision.gameObject);
        Destroy(projectile.gameObject);
    }
}
