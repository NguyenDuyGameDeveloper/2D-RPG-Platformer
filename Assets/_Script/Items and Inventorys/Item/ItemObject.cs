using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemDataSO itemData;

    ///private float timePassed;
    //private void Update()
    //{
    //    timePassed += Time.deltaTime;
    //    Debug.Log(timePassed);
    //}
    private void SetupVisuals()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        gameObject.name = "Item Object - " + itemData.itemName;
    }
    public void SetupItem(ItemDataSO _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetupVisuals();
    }
    public void PickupItem()
    {
        if (!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
        {
            rb.velocity = new Vector2(0, 10);
            PlayerManager.instance.player.playerFX.CreatePopUpText("Inventory is full");
            return;
        }
        AudioManager.instance.PlaySFX(16, transform);
        Inventory.instance.AddItemToInventory(itemData);

        //if(timePassed >= 10)
        //    Destroy(this.transform.gameObject);
        Destroy(gameObject);
    }
}
