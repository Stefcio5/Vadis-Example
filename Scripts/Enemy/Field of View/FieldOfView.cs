using Character;
using Enemy.Events;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class FieldOfView : MonoBehaviour
    {
        [Range(0, 360)] public float angle;
        public float detectionRadius;
        [SerializeField] private LayerMask detectionLayer;
        [SerializeField] private LayerMask obstacleMask;
        public bool canSeePlayer;
        public Transform currentTarget;
        public EnemyEvent OnPlayerFound;

        private void OnEnable()
        {
            currentTarget = FindObjectOfType<PlayerDamageManager>().transform;
            StartCoroutine(FOVRoutine());
        }

        private void OnDisable()
        {
            StopCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            float delay = 0.2f;
            WaitForSeconds wait = new WaitForSeconds(delay);
            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
            if (colliders.Length != 0)
            {
                Transform target = colliders[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    {
                        canSeePlayer = true;
                        OnPlayerFound.Raise();
                    }
                    else
                    {
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else if (canSeePlayer)
            {
                canSeePlayer = false;
            }
        }
    }
}