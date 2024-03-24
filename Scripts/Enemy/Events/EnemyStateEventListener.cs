using Enemy.Conditions;
using Enemy.States;
using System.Collections.Generic;

namespace Enemy.Events
{
    [System.Serializable]
    public class EnemyStateEventListener
    {
        public EnemyController enemyController;
        public BaseState nextState;
        public EnemyEvent Event;
        //public UnityEvent Response;
        public BaseStateEvent Response;
        public List<AICondition> conditions;

        public void OnEnable()
        {
            Event.RegisterListener(this);
        }
        public void OnDisable()
        {
            Event.UnregisterListener(this);
        }
        public void OnEventRaised()
        {
            if (CheckAllConditions())
            {
                Response.Invoke(nextState);
            }
        }
        private bool CheckAllConditions()
        {
            if (conditions.Count == 0)
            {
                return true;
            }
            foreach (var condition in conditions)
            {
                if (!condition.CheckCondition(enemyController))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
