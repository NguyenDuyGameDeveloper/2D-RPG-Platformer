using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int maxItemsToDrop;
    [SerializeField] private ItemDataSO[] itemPool;
    private List<ItemDataSO> possibleDrop = new List<ItemDataSO>();

    [SerializeField] private GameObject dropPrefab;

    public virtual void GenerateDrop()
    {
        if(itemPool.Length <= 0)
        {
            Debug.Log("Enemy not have any possible drop Item in the Pool to drop!");
            return;
        }

        foreach(ItemDataSO item in itemPool)
        {
            float randomInxex = Random.Range(0f, 100f);
            if(item != null && randomInxex < item.dropChance)
                possibleDrop.Add(item);
        }

        for(int i = 0; i < maxItemsToDrop; i++)
        {
            if(possibleDrop.Count > 0)
            {
                int randomIndex = Random.Range(0, possibleDrop.Count);
                ItemDataSO itemToDrop = possibleDrop[randomIndex];

                DropItem(itemToDrop);
                possibleDrop.Remove(itemToDrop);
            }
        }    
    }
    protected virtual void DropItem(ItemDataSO _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(10, 20));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
