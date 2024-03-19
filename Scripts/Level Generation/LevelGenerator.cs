using UnityEngine;
using Assets.Scripts.Level;
using System.Collections.Generic;
using Utils;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Level
{

    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private LevelDataSO _levelData;

        private Dictionary<Vector2Int, GameObject> _spawnedBlocks = new Dictionary<Vector2Int, GameObject>();

        private void Start()
        {
            GenerateMap();
            if (_levelData.generateBorder == true)
            {
                GenerateBorders();
            }
        }

        private Vector2Int GetRandomSpawnPointY(int x)
        {
            int y = Random.Range(0, _levelData.height);
            Vector2Int spawnPoint = new Vector2Int(x, y);
            return spawnPoint;
        }
        private Vector2Int GetRandomSpawnPoint(int minimumX, int maximumX)
        {
            int x = Random.Range(minimumX, maximumX);
            int y = Random.Range(0, _levelData.height);
            Vector2Int spawnPoint = new Vector2Int(x, y);
            if (!IsPositionOccupied(spawnPoint))
            {
                return spawnPoint;
            }
            else
            {
                return GetRandomSpawnPoint(minimumX, maximumX);
            }
        }

        private void GenerateBorders()
        {
            for (int x = -1; x < _levelData.width + 1; x++)
            {
                for (int y = -1; y < _levelData.height + 1; y++)
                {
                    if (!IsPositionOccupied(new Vector2Int(x, y)))
                    {
                        Quaternion quaternion = Quaternion.Euler(0, Random.Range(-1, 3) * 90, 0);
                        SpawnModule(_levelData.borderPrefab, new Vector2Int(x, y), quaternion);
                    }
                }

            }
        }

        public void GenerateMap()
        {
            RemoveAllBlocksImmediately();
            SpawnModule(_levelData.startPrefab, GetRandomSpawnPointY(0));
            SpawnModule(_levelData.endPrefab, GetRandomSpawnPointY(_levelData.width - 1));

            _levelData.blockadeData.Shuffle();
            var firstBlockade = _levelData.blockadeData[0];
            firstBlockade.blockadeTilePrefabs.Shuffle();
            var secondBlockade = _levelData.blockadeData[1];
            secondBlockade.blockadeTilePrefabs.Shuffle();

            foreach (GameObject blockadeEvent in firstBlockade.blockadeEventTilePrefabs)
            {
                Vector2Int spawnPoint = GetRandomSpawnPoint(_levelData.firstBlockadePosition.minBlockadeEventPosition.x, _levelData.firstBlockadePosition.maxBlockadeEventPosition.x);
                SpawnModule(blockadeEvent, spawnPoint);

            }
            foreach (GameObject blockadeEvent in secondBlockade.blockadeEventTilePrefabs)
            {
                Vector2Int spawnPoint = GetRandomSpawnPoint(_levelData.secondBlockadePosition.minBlockadeEventPosition.x, _levelData.secondBlockadePosition.maxBlockadeEventPosition.x);
                SpawnModule(blockadeEvent, spawnPoint);
            }

            for (int x = 0; x < _levelData.width; x++)
            {
                for (int y = 0; y < _levelData.height; y++)
                {
                    if (x == _levelData.firstBlockadePosition.blockadePosition.x)
                    {
                        SpawnModule(firstBlockade.blockadeTilePrefabs[y], new Vector2Int(x, y));
                    }

                    if (x == _levelData.secondBlockadePosition.blockadePosition.x)
                    {
                        SpawnModule(secondBlockade.blockadeTilePrefabs[y], new Vector2Int(x, y));
                    }
                    foreach (Tile normalMap in _levelData.normalMaps)
                    {
                        if (x >= normalMap.minPosition.x && x < normalMap.maxPosition.x && !IsPositionOccupied(new Vector2Int(x, y)))
                        {
                            GameObject randomTile = normalMap.tilePrefabs[Random.Range(0, normalMap.tilePrefabs.Count)];
                            Quaternion quaternion = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                            SpawnModule(randomTile, new Vector2Int(x, y), quaternion);
                        }
                    }
                }
            }
        }

        bool IsPositionOccupied(Vector2Int position)
        {
            if (_spawnedBlocks.ContainsKey(position))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void SpawnModule(GameObject modulePrefab, Vector2Int position)
        {
            _spawnedBlocks.Add(position, modulePrefab);
            GameObject module = Instantiate(modulePrefab, new Vector3(position.x * _levelData.offset, 0, position.y * _levelData.offset), Quaternion.identity);
            module.name = $"{modulePrefab.name} ({position.x}, {position.y})";
            module.transform.SetParent(this.transform);
        }
        void SpawnModule(GameObject modulePrefab, Vector2Int position, Quaternion quaternion)
        {
            _spawnedBlocks.Add(position, modulePrefab);
            GameObject module = Instantiate(modulePrefab, new Vector3(position.x * _levelData.offset, 0, position.y * _levelData.offset), quaternion);
            module.name = $"{modulePrefab.name} ({position.x}, {position.y})";
            module.transform.SetParent(transform);
        }

        public void RemoveAllBlocks()
        {
            _spawnedBlocks.Clear();
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        public void RemoveAllBlocksImmediately()
        {
            _spawnedBlocks.Clear();
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(LevelGenerator))]
    class BasicLevenGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var basicLevelGenerator = (LevelGenerator)target;
            if (basicLevelGenerator == null)
            {
                return;
            }
            if (GUILayout.Button("Generate Map"))
            {
                basicLevelGenerator.GenerateMap();
            }
            if (GUILayout.Button("Clear"))
            {
                basicLevelGenerator.RemoveAllBlocksImmediately();
            }
            DrawDefaultInspector();
        }
    }
#endif

    [System.Serializable]
    public class Tile
    {
        public TileType tileType;
        public List<GameObject> tilePrefabs;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;
    }

    [System.Serializable]
    public class Blockade
    {
        public Vector2Int blockadePosition;
        public Vector2Int minBlockadeEventPosition;
        public Vector2Int maxBlockadeEventPosition;
    }

    public enum TileType
    {
        Start,
        End,
        Normal,
        Blockade
    }
}