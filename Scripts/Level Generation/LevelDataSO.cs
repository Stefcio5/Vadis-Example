using System.Collections.Generic;
using Level;
using UnityEngine;


namespace Assets.Scripts.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
    public class LevelDataSO : ScriptableObject
    {
        [Header("Generator Settings")]
        public int width = 10;
        public int height = 4;
        public float offset = 1;
        [Header("Map Settings")]
        public GameObject startPrefab;
        public GameObject endPrefab;
        public List<BlockadeDataSO> blockadeData;
        public Blockade firstBlockadePosition;
        public Blockade secondBlockadePosition;
        public List<Tile> normalMaps;
        [Header("Border Settings")]
        public GameObject borderPrefab;
        public bool generateBorder;
    }
}