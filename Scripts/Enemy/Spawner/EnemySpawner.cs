using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.TestEnemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform enemyParent;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnInterval = 10f;
        [SerializeField] private int maxEnemies = 10;

        private List<GameObject> enemyPool = new List<GameObject>();
        private List<GameObject> spawnedEnemyPool = new List<GameObject>();
        private float spawnTimer;

        private void Start()
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation, enemyParent);
                enemy.SetActive(false);
                enemyPool.Add(enemy);
            }
            spawnTimer = spawnInterval;
        }

        private void Update()
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f && spawnedEnemyPool.Count < maxEnemies)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval;
            }
        }

        private void SpawnEnemy()
        {
            GameObject enemy = GetPooledEnemy();

            if (enemy != null)
            {
                Vector3 randomPosition = GetRandomNavMeshPosition();
                enemy.transform.position = randomPosition;
                enemy.SetActive(true);
                spawnedEnemyPool.Add(enemy);
            }
        }

        private GameObject GetPooledEnemy()
        {
            foreach (GameObject enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    return enemy;
                }
            }
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemyPool.Add(newEnemy);
            return newEnemy;
        }

        public void ReturnEnemyToPool(GameObject enemy)
        {
            enemy.SetActive(false);
            spawnedEnemyPool.Remove(enemy);
        }

        private Vector3 GetRandomNavMeshPosition()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            while (true)
            {
                Vector3 randomPosition = navMeshData.vertices[Random.Range(0, navMeshData.vertices.Length)];
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPosition, out hit, 1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
        }
    }
}