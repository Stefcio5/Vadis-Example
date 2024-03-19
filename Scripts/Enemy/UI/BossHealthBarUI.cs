using Enemy;
using UnityEngine;

namespace Enemy.UI
{
    public class BossHealthBarUI : HealthBarUI
    {
        [SerializeField] private EnemyStatsManager enemyStats;

        private void OnEnable()
        {
            enemyStats.OnHealthChanged += UpdateHealthBar;
        }
        private void OnDisable()
        {
            enemyStats.OnHealthChanged -= UpdateHealthBar;
        }

        public override void UpdateHealthBar(float currentHealth, float maxHealth) => slider.value = currentHealth / maxHealth;
    } 
}
