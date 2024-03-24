using Enemy.States;
using UnityEngine;

namespace Enemy.Conditions
{
    [CreateAssetMenu(fileName = "Condition", menuName = "Enemy/Conditions")]
    public class NotInStateCondition : AICondition
    {
        public BaseState state;
        public override bool CheckCondition(EnemyController enemyController)
        {
            return IsEnemyNotInState(enemyController);
        }

        public bool IsEnemyNotInState(EnemyController enemyController)
        {
            if (enemyController.currentState != state)
            {
                return true;
            }
            return false;
        }
    }
}
