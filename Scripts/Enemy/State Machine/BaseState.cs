using UnityEngine;

namespace Enemy.States
{
    public abstract class BaseState : ScriptableObject
    {
        public virtual void OnEnter(EnemyController enemyController)
        {
        }

        public virtual void OnUpdate(EnemyController enemyController)
        {
        }

        public virtual void OnExit(EnemyController enemyController)
        {
        }
    }
}