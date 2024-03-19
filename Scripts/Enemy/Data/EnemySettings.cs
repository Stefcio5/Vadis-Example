using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Data
{
    [CreateAssetMenu(fileName = "Enemy Settings", menuName = "Enemy/Enemy Settings")]
    public class EnemySettings : ScriptableObject
    {
        [Header("Settings")]
        [Tooltip("Range of walkable area")]
        public float wanderRange = 9f;

        [Tooltip("How long should enemy stay in idle state")]
        public float idleTime = 4f;

        [Tooltip("Range of chasing area")]
        public float chaseRange = 7f;

        [Tooltip("Range of attack")]
        public float attackRange = 2.5f;

        [Tooltip("Walking speed")]
        public float walkingSpeed = 1f;

        [Tooltip("Running / Chasing speed")]
        public float runningSpeed = 3f;

        [Tooltip("Rotation towards target speed")]
        public float rotationSpeed = 15f;

        [Tooltip("Face target angle")]
        public float faceTargetAngle = 15f;

        [Tooltip("Chance to perform combo attack")]
        public float comboAttackChance = 0f;
    }
}
