using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's drop")]
    [SerializeField] private float chanceToLooseItems;
    [SerializeField] private float chanceToLooseMaterials;

    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.instance;

        List<InventoryItem> itemsToDrop = new List<InventoryItem>();
        List<InventoryItem> materialToDrop = new List<InventoryItem>();

        DropItemsEquipment(inventory, itemsToDrop);
        DropItemsMaterial(inventory, materialToDrop);
    }
    private void DropItemsEquipment(Inventory inventory, List<InventoryItem> itemsToDrop)
    {
        foreach (InventoryItem item in inventory.GetEquipmentList())
        {
            if (Random.Range(0, 100) <= chanceToLooseItems)
            {
                DropItem(item.data);
                itemsToDrop.Add(item);
            }
        }

        for (int i = 0; i < itemsToDrop.Count; i++)
        {
            inventory.UnequipItem(itemsToDrop[i].data as ItemData_Equipment);
        }
    }
    private void DropItemsMaterial(Inventory inventory, List<InventoryItem> materialToDrop)
    {
        foreach (InventoryItem material in inventory.GetMaterialList())
        {
            if (Random.Range(0, 100) <= chanceToLooseMaterials)
            {
                DropItem(material.data);
                materialToDrop.Add(material);
            }
        }

        for (int i = 0; i < materialToDrop.Count; i++)
        {
            inventory.RemoveItem(materialToDrop[i].data);
        }
    }
}
