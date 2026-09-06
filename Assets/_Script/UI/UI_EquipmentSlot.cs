using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType slotType;

    private void OnValidate()
    {
        gameObject.name = "Equipment slot - " + slotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null)
        {
            Debug.Log("EquipmentSlot class call");
            return; // || item.data == null
        }
        Inventory.instance.UnequipItem(item.data as ItemData_Equipment);
        Inventory.instance.AddItemToInventory(item.data as ItemData_Equipment);
        CleanUpSlot();

        ui.itemToolTip.HideToolTip();
    }

}
