using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField]
    private List<LootTable> _lootTables;
    [SerializeField]
    private int _numberOfDrops = 1;
    [SerializeField]
    private float _dropRadius = 1f;
    [SerializeField]
    private float _launchForce;
    [SerializeField]
    private List<GameObject> _droppedItems;
    private Vector3 _dropPosition;

    private void Start()
    {
        _droppedItems = new List<GameObject>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ClearDroppedItems();
        }
    }

    public void DropLoot()
    {
        for (int i = 0; i < _numberOfDrops; i++)
        {
            LootTable lootTable = SelectLootTable();
            GameObject itemPrefab = SelectItemFromTable(lootTable);

            if (itemPrefab != null)
            {
                SpawnItem(itemPrefab);
            }
            else
            {
                Debug.Log("No items dropped");
            }
        }
    }
    public void SpawnItem(GameObject itemPrefab)
    {
        _dropPosition.x = transform.position.x + Random.Range(-_dropRadius, _dropRadius);
        _dropPosition.y = transform.position.y + 1f;
        _dropPosition.z = transform.position.z + Random.Range(-_dropRadius, _dropRadius);
        GameObject droppedItem = ObjectPoolManager.instance.SpawnGameObject(itemPrefab, _dropPosition, Quaternion.identity);
        droppedItem.GetComponent<Rigidbody>()?.AddForce(transform.up * _launchForce, ForceMode.Impulse);
        _droppedItems.Add(droppedItem);
    }

    private LootTable SelectLootTable()
    {
        int totalWeight = 0;
        foreach (LootTable lootTable in _lootTables)
        {
            totalWeight += lootTable.weight;
        }

        int randomWeight = Random.Range(0, totalWeight);
        int weightSum = 0;

        foreach (LootTable lootTable in _lootTables)
        {
            weightSum += lootTable.weight;
            if (randomWeight < weightSum)
            {
                return lootTable;
            }
        }
        return _lootTables[0];
    }

    private GameObject SelectItemFromTable(LootTable lootTable)
    {
        int totalWeight = 0;
        foreach (LootTableEntry entry in lootTable.lootTableEntries)
        {
            totalWeight += entry.weight;
        }

        int randomWeight = Random.Range(0, totalWeight);
        int weightSum = 0;

        foreach (LootTableEntry entry in lootTable.lootTableEntries)
        {
            weightSum += entry.weight;
            if (randomWeight < weightSum)
            {
                return entry.itemPrefab;
            }
        }
        return lootTable.lootTableEntries[0].itemPrefab;
    }

    public void ClearDroppedItems()
    {
        foreach (GameObject droppedItem in _droppedItems)
        {   
            ObjectPoolManager.instance.EmptyPool(droppedItem);
            Destroy(droppedItem);
        }
        _droppedItems.Clear();
    }
}

