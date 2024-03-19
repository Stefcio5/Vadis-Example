using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy.UI
{
    public class EnemyHealthBarUI : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private EnemyStatsManager enemyStats;
        [SerializeField] private Slider slider;
        private void Start()
        {
            camera = FindObjectOfType<Camera>();
        }

        private void OnEnable()
        {
            enemyStats.OnHealthChanged += UpdateHealthBar;
        }
        private void OnDisable()
        {
            enemyStats.OnHealthChanged -= UpdateHealthBar;
        }

        public virtual void UpdateHealthBar(float currentHealth, float maxHealth) => slider.value = currentHealth / maxHealth;


        private void Update()
        {
            transform.rotation = camera.transform.rotation;
            transform.position = target.position + offset;
        }
    }
}
