using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrievalGun : Gun
{
    protected override void Projectile_OnTriggerEnter2D(Projectile projectile, Collider2D collider)
    {
        InventoryManager.tryAddToInventory(gameObject, collider.gameObject);
        Destroy(projectile.gameObject);
    }
}
