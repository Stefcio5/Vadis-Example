using System.Collections.Generic;
using UnityEngine;
namespace Level
{

    [CreateAssetMenu(fileName = "BlockadeDataSO", menuName = "Level/BlockadeDataSO", order = 0)]
    public class BlockadeDataSO : ScriptableObject
    {
        public List<GameObject> blockadeTilePrefabs;
        public List<GameObject> blockadeEventTilePrefabs;
    }
}