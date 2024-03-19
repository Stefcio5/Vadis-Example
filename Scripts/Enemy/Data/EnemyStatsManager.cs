using Combat;
using Enemy.TestEnemy;
using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyStatsManager : MonoBehaviour, IDamageable
    {
        private EnemySpawner enemySpawner;
        public int currentHealth;
        public int maxHealth;
        public int attack;

        public event Action<float, float> OnHealthChanged;

        private void Start()
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        private void OnEnable()
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void Die()
        {
            enemySpawner.ReturnEnemyToPool(gameObject);
        }
    }
}