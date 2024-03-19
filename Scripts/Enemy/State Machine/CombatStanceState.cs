using UnityEngine;


namespace Enemy.States
{
    [CreateAssetMenu(fileName = "Combat Stance State", menuName = "Enemy/Enemy States/Combat Stance State")]
    public class CombatStanceState : BaseState
    {
        public BaseState AttackState;
        public BaseState ChaseState;
        public BaseState WanderState;
        public override void OnEnter(EnemyController enemyController)
        {
            base.OnEnter(enemyController);
            enemyController.Agent.isStopped = true;
            enemyController.Animator.SetBool("Combat Idle", true);
        }

        public override void OnUpdate(EnemyController enemyController)
        {
            base.OnUpdate(enemyController);
            enemyController.FaceTarget(enemyController.playerTarget.transform.position);

            if (enemyController.IsPlayerInAttackRange() && enemyController.IsFacingTarget(enemyController.playerTarget.transform.position))
            {
                enemyController.SwitchState(AttackState);
            }
            else if (enemyController.IsPlayerInChaseRange() && !enemyController.IsPlayerInAttackRange())
            {
                enemyController.SwitchState(ChaseState);
            }

            else if (!enemyController.IsPlayerInAttackRange() && !enemyController.IsPlayerInChaseRange())
            {
                enemyController.SwitchState(WanderState);
            }

        }
        public override void OnExit(EnemyController enemyController)
        {
            base.OnExit(enemyController);
            enemyController.Agent.isStopped = false;
            enemyController.Animator.SetBool("Combat Idle", false);
        }
    }
}
