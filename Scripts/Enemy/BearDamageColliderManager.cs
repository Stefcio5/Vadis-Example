using UnityEngine;

namespace Enemy
{
    public class BearDamageColliderManager : MonoBehaviour
    {
        [SerializeField] private DamageCollider rightHandCollider;
        [SerializeField] private DamageCollider leftHandCollider;
        [SerializeField] private DamageCollider jawCollider;

        public void ActivateRightDamageCollider()
        {
            rightHandCollider.EnableDamageCollider();
        }
        public void ActivateLeftDamageCollider()
        {
            leftHandCollider.EnableDamageCollider();
        }
        public void ActivateJawDamageCollider()
        {
            jawCollider.EnableDamageCollider();
        }
        public void DeactivateRightDamageCollider()
        {
            rightHandCollider.DisableDamageCollider();
        }
        public void DeactivateLeftDamageCollider()
        {
            leftHandCollider.DisableDamageCollider();
        }
        public void DeactivateJawDamageCollider()
        {
            jawCollider.DisableDamageCollider();
        }
    }
}
