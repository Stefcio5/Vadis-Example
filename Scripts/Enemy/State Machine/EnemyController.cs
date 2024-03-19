using Enemy.Data;
using Enemy.Attacks;
using Enemy.States;
using UnityEngine;
using UnityEngine.AI;
using Enemy.Events;
using System.Collections.Generic;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyController : MonoBehaviour
    {
        public BaseState defaultState;

        [Header("References")]
        private Animator _animator;

        private NavMeshAgent _agent;
        private FieldOfView _fieldOfView;

        [Tooltip("Player prefab")]
        [HideInInspector]
        public Transform playerTarget;

        [Tooltip("Centre point of walkable area")]
        private Transform _centrePoint;

        public Ability[] attackAbilities;
        public Ability[] comboAttacks;

        [HideInInspector]
        public bool isAttacking;
        [HideInInspector]
        public BaseState currentState;
        [Header("Settings")]
        public EnemySettings enemySettings;

        public Animator Animator { get => _animator; private set => _animator = value; }
        public NavMeshAgent Agent { get => _agent; set => _agent = value; }
        public Transform CentrePoint { get => _centrePoint; private set => _centrePoint = value; }
        public FieldOfView FieldOfView { get => _fieldOfView; private set => _fieldOfView = value; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            FieldOfView = GetComponent<FieldOfView>();
            CentrePoint = GameObject.Find("Enemy CentrePoint").transform;
        }

        private void Start()
        {
            SwitchState(defaultState);
            playerTarget = FieldOfView.currentTarget;
        }

        private void Update()
        {
            currentState?.OnUpdate(this);
        }

        public void SwitchState(BaseState state)
        {
            currentState?.OnExit(this);
            currentState = state;
            currentState.OnEnter(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(CentrePoint.position, enemySettings.wanderRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, enemySettings.chaseRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, enemySettings.attackRange);
        }

        public bool IsPlayerInChaseRange() => Vector3.Distance(transform.position, playerTarget.position) <= enemySettings.chaseRange;

        public bool IsPlayerInAttackRange() => Vector3.Distance(transform.position, playerTarget.position) <= enemySettings.attackRange;

        public void FaceTarget(Vector3 target)
        {
            Vector3 lookPos = target - transform.position;
            lookPos.y = 0;
            lookPos.Normalize();

            if (lookPos == Vector3.zero)
            {
                lookPos = transform.forward;
            }

            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, enemySettings.rotationSpeed * Time.deltaTime);
        }

        public bool IsFacingTarget(Vector3 target)
        {
            Vector3 targetDirection = target - transform.position;
            targetDirection.y = 0f;

            Vector3 enemyForward = transform.forward;
            enemyForward.y = 0f;

            float angle = Vector3.Angle(enemyForward, targetDirection);
            return angle <= enemySettings.faceTargetAngle;
        }
    }
}