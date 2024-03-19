using Enemy.Attacks;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(fileName = "Attack State", menuName = "Enemy/Enemy States/Attack State")]
    public class AttackState : BaseState
    {
        private float attackCooldown;
        private bool hasAttacked;
        private float comboChanceRoll;
        private Ability currentAttack;
        public BaseState CombatStanceState;

        public override void OnEnter(EnemyController enemyController)
        {
            base.OnEnter(enemyController);
            enemyController.Agent.isStopped = true;
            enemyController.Animator.SetBool("Combat Idle", true);
            GetCurrentAttack(enemyController);
            attackCooldown = currentAttack.cooldownTime;
            hasAttacked = false;
            enemyController.isAttacking = true;
            Attack(enemyController);
        }

        public override void OnUpdate(EnemyController enemyController)
        {
            base.OnUpdate(enemyController);
            if (hasAttacked)
            {
                if (attackCooldown <= 0)
                {
                    enemyController.SwitchState(CombatStanceState);
                }
                attackCooldown -= Time.deltaTime;
            }
        }


        public override void OnExit(EnemyController enemyController)
        {
            enemyController.Animator.SetBool("Combat Idle", false);
            enemyController.Agent.isStopped = false;
            enemyController.isAttacking = false;
        }
        public void GetCurrentAttack(EnemyController enemyController)
        {
            if (GetComboChanceRoll() <= enemyController.enemySettings.comboAttackChance)
            {
                currentAttack = enemyController.comboAttacks[Random.Range(0, enemyController.comboAttacks.Length)];
            }
            else
            {
                currentAttack = enemyController.attackAbilities[Random.Range(0, enemyController.attackAbilities.Length)];
            }
        }
        public void Attack(EnemyController enemyController)
        {
            currentAttack.Activate(enemyController.Animator);
            hasAttacked = true;
        }
        public float GetComboChanceRoll()
        {
            comboChanceRoll = Random.Range(1, 101);
            return comboChanceRoll;
        }
    }
}
