using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Attacks
{
    [CreateAssetMenu(menuName = "Ability")]
    public class Ability : ScriptableObject
    {

        public string abilityName;
        public float cooldownTime;
        public string animationString;
        public float animationDuration;

        public void Activate(Animator animator)
        {

            animator.SetTrigger(animationString);
        }
    }
}
