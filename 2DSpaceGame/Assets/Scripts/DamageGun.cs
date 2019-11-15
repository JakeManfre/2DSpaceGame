﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGun : Gun
{
    protected override void Projectile_OnTriggerEnter2D(Projectile projectile, Collider2D collider)
    {
        Destroy(projectile.gameObject);
        Destroy(collider.gameObject);
    }
}
