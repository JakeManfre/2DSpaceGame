using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrievalGun : Gun
{
    protected override void Projectile_OnTriggerEnter2D(Projectile projectile, Collider2D collider)
    {
        // Get the Pickup component
        Pickup pickupComponent = collider.GetComponent<Pickup>();
        if (pickupComponent == null) { return; }

        Inventory inventory = myOwner.GetComponent<Inventory>();
        if (inventory == null) { return; }

        // Add it to the inventory
        InventoryManager.addToInventory(inventory, pickupComponent);
    }
}
