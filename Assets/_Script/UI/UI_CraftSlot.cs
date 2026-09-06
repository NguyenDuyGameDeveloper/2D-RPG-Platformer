using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    protected override void Start()
    {
        base.Start();
    }
    public void SetpCraftSlot(ItemData_Equipment _data)
    {
        if(_data == null) return;

        item.data = _data;

        itemImage.sprite = _data.itemIcon;
        itemText.text = _data.itemName;

        if (itemText.text.Length > 30)
            itemText.fontSize *= .9f;
        else
            itemText.fontSize = 20;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
    }
}
