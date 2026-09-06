using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    #region Info
    public static Inventory instance;

    public List<ItemDataSO> startingItems;

    public List<InventoryItem> inventory;
    public Dictionary<ItemDataSO, InventoryItem> inventoryDictionary;

    public List<InventoryItem> material;
    public Dictionary<ItemDataSO, InventoryItem> materialDictionary;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;
    #endregion
    #region UI
    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform materialSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;

    private UI_ItemSlot[] inventoryItemSlot;
    private UI_ItemSlot[] materialItemSlot;
    private UI_EquipmentSlot[] equipmentItemSlot;
    private UI_StatSlot[] statSlots;

    #endregion
    #region Item
    [Header("Item cooldown")]
    private float lastTimeUsePotion;
    private float lastTimeUseArmor;

    public float potionCooldown { get; private set; }
    private float armorCooldown;

    [Header("Data base")]
    public List<ItemDataSO> itemDataBase;
    public List<InventoryItem> loadedItems;
    public List<InventoryItem> loadedMaterial; //Del
    public List<ItemData_Equipment> loadedEquipment;
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemDataSO, InventoryItem>();

        material = new List<InventoryItem>();
        materialDictionary = new Dictionary<ItemDataSO, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        materialItemSlot = materialSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentItemSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlots = statSlotParent.GetComponentsInChildren<UI_StatSlot>();

        AddStartingItems();
    }
    private void AddStartingItems()
    {
        if (loadedItems.Count > 0 || loadedMaterial.Count > 0 || loadedEquipment.Count > 0)
        {

            foreach (InventoryItem item in loadedItems)
            {
                for (int i = 0; i < item.stackSize; i++)
                {
                    AddItemToInventory(item.data);
                }
            }

            foreach (InventoryItem material in loadedMaterial)
            {
                for (int i = 0; i < material.stackSize; i++)
                {
                    AddItemToInventory(material.data);
                }
            }

            foreach (ItemData_Equipment equipment in loadedEquipment)
            {
                EquipItem(equipment);
            }

            return;
        }

        for (int i = 0; i < startingItems.Count; i++)
        {
            if (startingItems[i] != null)
                AddItemToInventory(startingItems[i]);
        }

    }
    public void EquipItem(ItemDataSO _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        ItemData_Equipment oldEquipment = null;

        InventoryItem newItem = new(newEquipment);

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
                oldEquipment = item.Key;
        }

        if (oldEquipment != null)
        {
            UnequipItem(oldEquipment);
            AddItemToInventory(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        RemoveItem(_item);
        UpdateSlotUI();
    }
    public void UnequipItem(ItemData_Equipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }
    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentItemSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentItemSlot[i].slotType)
                    equipmentItemSlot[i].UpdateSlot(item.Value);
            }
        }

        for (int i = 0; i < inventoryItemSlot.Length; i++) //Update UI Inventory
        {
            inventoryItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < materialItemSlot.Length; i++) // Update UI Material
        {
            materialItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < material.Count; i++)
        {
            materialItemSlot[i].UpdateSlot(material[i]);
        }

        UpdateStatsUI();
    }
    public void UpdateStatsUI()
    {
        for (int i = 0; i < statSlots.Length; i++) //Update UI Character Stat
        {
            statSlots[i].UpdateStatValueUI();
        }
    }
    private void AddToMaterialInventory(ItemDataSO _item)
    {
        if (materialDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            material.Add(newItem);
            materialDictionary.Add(_item, newItem);
        }
    }
    private void AddToEquipmentInventory(ItemDataSO _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }
    public bool CanAddItem()
    {
        if (inventory.Count >= inventoryItemSlot.Length)
            return false;
        return true;
    }
    public void AddItemToInventory(ItemDataSO _item)
    {
        if (_item.itemType == ItemType.Equipment && CanAddItem())
            AddToEquipmentInventory(_item);
        else if (_item.itemType == ItemType.Material)
            AddToMaterialInventory(_item);

        UpdateSlotUI();
    }
    public void RemoveItem(ItemDataSO _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem equipmentValue))
        {
            if (equipmentValue.stackSize <= 1)
            {
                inventory.Remove(equipmentValue);
                inventoryDictionary.Remove(_item);
            }
            else
                equipmentValue.RemoveStack();
        }
        if (materialDictionary.TryGetValue(_item, out InventoryItem materialValue))
        {
            if (materialValue.stackSize <= 1)
            {
                material.Remove(materialValue);
                materialDictionary.Remove(_item);
            }
            else
                materialValue.RemoveStack();
        }
        UpdateSlotUI();
    }
    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requiredMaterials)
    {
        foreach (var requiredItem in _requiredMaterials) // Check if all required materials are available 
        {
            if(materialDictionary.TryGetValue(requiredItem.data, out InventoryItem materialItem))
            {
                if (materialItem.stackSize < requiredItem.stackSize)
                {
                    Debug.Log("Not enough Material: " + requiredItem.data.name);
                    return false;
                }
            }
            else
            {
                Debug.Log("Material is not found!" + requiredItem.data.name);
            }
        }

        foreach (var requiredMaterial in _requiredMaterials) // If all materials are available, remove it from Material Inventory
        {
            for (int i = 0; i < requiredMaterial.stackSize; i++)
            {
                RemoveItem(requiredMaterial.data);
            }
        }
        AddItemToInventory(_itemToCraft);
        Debug.Log("Craft successful: " +  _itemToCraft.name);
        return true;
    }
    public List<InventoryItem> GetEquipmentList() => equipment;
    public List<InventoryItem> GetMaterialList() => material;
    public ItemData_Equipment GetEquipment(EquipmentType _type)
    {
        ItemData_Equipment equipItem = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
                equipItem = item.Key;
        }
        return equipItem;
    }
    public void UsePotion()
    {
        ItemData_Equipment currentPotion = GetEquipment(EquipmentType.Potion);

        if (!currentPotion) return;

        bool canUsePotion = Time.time > lastTimeUsePotion + potionCooldown;

        if (canUsePotion)
        {
            potionCooldown = currentPotion.itemCooldown;
            currentPotion.Effect(null);
            lastTimeUsePotion = Time.time;
        }
        else
            Debug.Log("Potion on cooldown");
    }
    public bool CanUseArmorEffect()
    {
        ItemData_Equipment currentArmor = GetEquipment(EquipmentType.Armor);

        if (Time.time > lastTimeUseArmor + armorCooldown)
        {
            armorCooldown = currentArmor.itemCooldown;
            lastTimeUseArmor = Time.time;
            return true;
        }
        Debug.Log("Effect Freeze on cooldown!");
        return false;
    }
    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && item.itemId == pair.Key)
                {
                    InventoryItem itemToLoad = new(item)
                    {
                        stackSize = pair.Value
                    };

                    loadedItems.Add(itemToLoad);
                }
            }
        }

        foreach (KeyValuePair<string, int> pair in _data.material)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && item.itemId == pair.Key)
                {
                    InventoryItem materialToLoad = new(item)
                    {
                        stackSize = pair.Value
                    };

                    loadedMaterial.Add(materialToLoad);
                }
            }
        }

        foreach (string loadedItemId in _data.equipmentId)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && loadedItemId == item.itemId)
                {
                    loadedEquipment.Add(item as ItemData_Equipment);
                }
            }
        }
    }
    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.material.Clear();
        _data.equipmentId.Clear();

        foreach (KeyValuePair<ItemDataSO, InventoryItem> pair in inventoryDictionary)
        {
            _data.inventory.Add(pair.Key.itemId, pair.Value.stackSize);
        }

        foreach (KeyValuePair<ItemDataSO, InventoryItem> pair in materialDictionary)
        {
            _data.material.Add(pair.Key.itemId, pair.Value.stackSize);
        }

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> pair in equipmentDictionary)
        {
            _data.equipmentId.Add(pair.Key.itemId);
        }
    }
#if UNITY_EDITOR
    [ContextMenu("Fill Up Item Data Base")]
    private void FillUpItemDataBase() => itemDataBase = new List<ItemDataSO>(GetItemDataBase());
    private List<ItemDataSO> GetItemDataBase()
    {
        List<ItemDataSO> itemDataBase = new();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" });

        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemDataSO>(SOpath);

            itemDataBase.Add(itemData);
        }
        return itemDataBase;
    }
#endif
}
