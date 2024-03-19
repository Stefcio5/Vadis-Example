using UnityEngine;
using UnityEngine.AI;

namespace Enemy.States
{
    [CreateAssetMenu(fileName = "Wander State", menuName = "Enemy/Enemy States/Wander State")]
    public class WanderState : BaseState
    {
        private bool _hasReachedDestination;
        public BaseState IdleState;

        public override void OnEnter(EnemyController enemyController)
        {
            base.OnEnter(enemyController);
            _hasReachedDestination = false;
            enemyController.Animator.SetBool("WalkForward", true);
            enemyController.Agent.speed = enemyController.enemySettings.walkingSpeed;
        }

        public override void OnUpdate(EnemyController enemyController)
        {
            base.OnUpdate(enemyController);
            if (enemyController.Agent.remainingDistance <= enemyController.Agent.stoppingDistance)
            {
                if (_hasReachedDestination)
                {
                    enemyController.SwitchState(IdleState);
                }
                Vector3 point;
                if (RandomPoint(enemyController.CentrePoint.position, enemyController.enemySettings.wanderRange, out point) && !_hasReachedDestination)
                {
                    enemyController.Agent.SetDestination(point);
                    _hasReachedDestination = true;
                }
            }
        }
        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
            result = Vector3.zero;
            return false;
        }

        public override void OnExit(EnemyController enemyController)
        {
            base.OnExit(enemyController);
            enemyController.Animator.SetBool("WalkForward", false);
        }
    }
}
