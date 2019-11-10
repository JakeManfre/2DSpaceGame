using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static bool addToInventory(Inventory inventory, Pickup item)
    {
        // Bad data
        if (!inventory || !item) { return false; }

        // Can't fit
        if (!inventory.add(item)) { return false; }

        // Do things to objects
        item.soundOnPickUp();
        item.gameObject.SetActive(false);

        return true;
    }
    public static bool removeFromInventory(Inventory inventory, Pickup item)
    {
        // Bad data
        if (!inventory || !item) { return false; }

        // Can't fit
        if (!inventory.remove(item)) { return false; }

        // Do things to objects
        item.soundOnDrop();
        item.gameObject.SetActive(true);
        return true;
    }
}
