using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public static bool AddToInventory(Inventory inventory, Pickup item)
    {
        // Bad data
        if (!inventory || !item) { return false; }

        // Can't fit
        if (!inventory.Add(item)) { return false; }

        // Do things to objects
        item.soundOnPickUp();
        item.gameObject.SetActive(false);

        return true;
    }
    public static bool RemoveFromInventory(Inventory inventory, Pickup item)
    {
        // Bad data
        if (!inventory || !item) { return false; }

        // Can't fit
        if (!inventory.Remove(item)) { return false; }

        // Do things to objects
        item.soundOnDrop();
        item.gameObject.SetActive(true);
        return true;
    }

    public static bool TryAddToInventory(Inventory inventory, GameObject shouldBePickable)
    {
        Pickup pickupComponent = shouldBePickable.GetComponent<Pickup>();
        return AddToInventory(inventory, pickupComponent);
    }

    public static bool TryAddToInventory(GameObject shouldHaveInventory, GameObject shouldBePickable)
    {
        Inventory inventory = shouldHaveInventory.GetComponentInParent<Inventory>();
        return TryAddToInventory(inventory, shouldBePickable);
    }
}
