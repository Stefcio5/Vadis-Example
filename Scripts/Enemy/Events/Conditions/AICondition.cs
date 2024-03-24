using UnityEngine;

namespace Enemy.Conditions
{   
    public abstract class AICondition : ScriptableObject
    {
        public abstract bool CheckCondition(EnemyController enemyController);
    }
}