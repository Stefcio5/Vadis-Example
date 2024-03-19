using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy.UI
{
    public abstract class HealthBarUI : MonoBehaviour
    {
        public Slider slider;

        private void OnEnable()
        {
            StatsManager.OnHealthChanged += UpdateHealthBar;
        }
        private void OnDisable()
        {
            StatsManager.OnHealthChanged -= UpdateHealthBar;
        }

        public virtual void UpdateHealthBar(float currentHealth, float maxHealth) => slider.value = currentHealth / maxHealth;
    } 
}
