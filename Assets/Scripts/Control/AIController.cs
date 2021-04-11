using RPG.Characters;
using RPG.Combat;
using RPG.Movement;
using RPG.StateManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class AIController : BaseController
    {
        [SerializeField]
        private PatrolPath patrolPath;
        private GameObject playerGameObject;
        private Player player;
        private Enemy enemy;
        private Fighter fighter;
        private Mover mover;
        private IEnumerator suspiciousCoroutine;

        private IEnumerator resetPatrolCoroutine;
        private IEnumerator dwelInWayPointCoroutine;

        [SerializeField]
        private EmptyPatrolPathGenerator patrolPathGenerator;

        [SerializeField]
        private float toleranceStopDistance = 0.5f;

        [SerializeField]
        private float dwelInPoint = 1f;

        public delegate void OnSuspicousFinishedDelegate();
        public event OnSuspicousFinishedDelegate OnSuspicousFinishedEvent;

        private void Awake()
        {
            CatchCoRoutines();
            CatchPatrolPath();
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            player = playerGameObject.GetComponent<Player>();
            enemy = GetComponent<Enemy>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
        }

        private void CatchPatrolPath()
        {
            if (patrolPath == null) patrolPath = patrolPathGenerator.CreateEmptyPatrolPath(transform.position);
        }

        private void Start()
        {
            SpawnEnemyAtInitPath();
        }

        private void SpawnEnemyAtInitPath()
        {
            transform.position = patrolPath.GetNextWayPoint();
        }

        // The catching is simply to avoid null checks
        private void CatchCoRoutines()
        {
            suspiciousCoroutine = ActivateSuspicousBehaviour();
            resetPatrolCoroutine = StartPatrolBehaviour();
            dwelInWayPointCoroutine = StartDwelInPoint();
        }

        private void DeadBehaviour()
        {
            fighter.CancelAttack();
            mover.DisableMover();
        }

        private void PatrolBehaviour()
        {
            fighter.CancelAttack();

            resetPatrolCoroutine = StartPatrolBehaviour();

            StartCoroutine(resetPatrolCoroutine);
        }

        private IEnumerator StartPatrolBehaviour()
        {
            Vector3 nextWayPoint = patrolPath.GetNextWayPoint();
            
            mover.MoveTo(nextWayPoint, enemy.PatrolSpeed, 0);

            while(Vector3.Distance(transform.position, nextWayPoint) > toleranceStopDistance)
            {
                yield return null;
            }

            dwelInWayPointCoroutine = StartDwelInPoint();
            StartCoroutine(dwelInWayPointCoroutine);
        }

        private IEnumerator StartDwelInPoint()
        {
            float totalTime = 0;
            while(totalTime <= dwelInPoint)
            {
                totalTime += Time.deltaTime;
                yield return null;
            }

            resetPatrolCoroutine = StartPatrolBehaviour();
            StartCoroutine(resetPatrolCoroutine);
        }

        private void AttackPlayerBehaviour()
        {
            CancelPatrolBehaviour();
            fighter.Attack(player, enemy);
        }

        private void CancelPatrolBehaviour()
        {
            StopCoroutine(resetPatrolCoroutine);
            StopCoroutine(dwelInWayPointCoroutine);
        }

        IEnumerator ActivateSuspicousBehaviour()
        {
            float totalTime = 0;
            while (totalTime <= enemy.SuspiciousDuration)
            {
                totalTime += Time.deltaTime;
                yield return null;
            }

            OnSuspicousFinishedEvent?.Invoke();
        }

        public override void OnStateChanged(Enum state)
        {
            EnemyState enemyState = (EnemyState) state;

            switch (enemyState) {

                case EnemyState.Dead:
                {
                    DeadBehaviour();
                    return;
                }

                case EnemyState.Attack:
                {
                    AttackPlayerBehaviour();
                    break;
                }

                case EnemyState.Patrol:
                {
                    PatrolBehaviour();
                    break;
                }

                case EnemyState.Suspicious:
                {
                    SuspiciousBehaviour();
                    break;
                }
            }
        }

        private void SuspiciousBehaviour()
        {
            fighter.CancelAttack();

            StopCoroutine(suspiciousCoroutine);

            suspiciousCoroutine = ActivateSuspicousBehaviour();

            StartCoroutine(suspiciousCoroutine);
        }
    }

}
