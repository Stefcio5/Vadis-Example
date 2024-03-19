using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(fileName = "Chase State", menuName = "Enemy/Enemy States/Chase State")]
    public class ChaseState : BaseState
    {
        public BaseState CombatStanceState;
        public BaseState WanderState;
        public override void OnEnter(EnemyController enemyController)
        {
            base.OnEnter(enemyController);
            enemyController.Animator.SetBool("Run Forward", true);
            enemyController.Agent.speed = enemyController.enemySettings.runningSpeed;
        }

        public override void OnUpdate(EnemyController enemyController)
        {
            base.OnUpdate(enemyController);
            if (!enemyController.IsPlayerInChaseRange())
            {
                enemyController.SwitchState(WanderState);
            }


            if (enemyController.IsPlayerInChaseRange())
            {
                if (enemyController.IsPlayerInAttackRange())
                {
                    enemyController.SwitchState(CombatStanceState);
                }
                enemyController.Agent.SetDestination(enemyController.playerTarget.position);
            }
        }

        public override void OnExit(EnemyController enemyController)
        {
            enemyController.Animator.SetBool("Run Forward", false);
            enemyController.Agent.speed = enemyController.enemySettings.walkingSpeed;
        }
    }
}
