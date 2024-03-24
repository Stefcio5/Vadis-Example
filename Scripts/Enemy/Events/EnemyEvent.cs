using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Events
{
    [CreateAssetMenu(fileName = "Enemy Event", menuName = "Enemy/Enemy Event")]
    public class EnemyEvent : ScriptableObject
    {
        private List<EnemyStateEventListener> listeners = new List<EnemyStateEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }
        public void RegisterListener(EnemyStateEventListener listener)
        {
            listeners.Add(listener);
        }
        public void UnregisterListener(EnemyStateEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
