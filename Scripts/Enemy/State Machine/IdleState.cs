using Enemy.Events;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "Enemy/Enemy States/Idle State")]
    public class IdleState : BaseState
    {
        public EnemyEvent OnIdleTimeElapsed;
        public int chanceToPerformIdleAction;
        private float _timer;
        private int _randomNumber;
        public BaseState WanderState;
        public override void OnEnter(EnemyController enemyController)
        {
            base.OnEnter(enemyController);
            GetRandomNumber();
        }
        public override void OnUpdate(EnemyController enemyController)
        {
            base.OnUpdate(enemyController);
            _timer += Time.deltaTime;
            if (_randomNumber <= chanceToPerformIdleAction)
            {
                enemyController.Animator.SetBool("Eat", true);

            }
            if (_timer >= enemyController.enemySettings.idleTime)
            {
                _timer = 0f;
                enemyController.SwitchState(WanderState);
            }
        }

        public override void OnExit(EnemyController enemyController)
        {
            base.OnExit(enemyController);
            enemyController.Animator.SetBool("Eat", false);
        }
        private void GetRandomNumber()
        {
            _randomNumber = Random.Range(0, 101);
        }
    }
}
