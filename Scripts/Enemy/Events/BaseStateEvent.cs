using Enemy.States;
using UnityEngine.Events;

namespace Enemy.Events
{
    [System.Serializable]
    public class BaseStateEvent : UnityEvent<BaseState>
    {
    }
}