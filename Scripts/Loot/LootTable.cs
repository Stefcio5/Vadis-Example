using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    public string tableName;
    public int weight;
    public List<LootTableEntry> lootTableEntries;
}
