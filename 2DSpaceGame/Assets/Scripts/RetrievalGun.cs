using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrievalGun : Gun
{
    // Start is called before the first frame update
    void Start()
    {
        base.overrideProjectileTriggerCallback(this.Projectile_OnTrigger2DEnter);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void Projectile_OnTrigger2DEnter(Projectile shot, Collider2D collided)
    {
        if (shot == null || collided == null) { return; }

        GameObject collidedGameObject = collided.gameObject;
        if (collidedGameObject == null) { return; }

        Pickup pickupComponent = collidedGameObject.GetComponent<Pickup>();
        if (pickupComponent == null) { return; }

        Inventory inventory = gameObject.GetComponent<Inventory>();
        if (inventory == null) { return; }

        InventoryManager.addToInventory(inventory, pickupComponent);
    }
}
