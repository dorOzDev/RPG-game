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

        private GameObject playerGameObject;
        private Player player;
        private Enemy enemy;
        private Fighter fighter;
        private Mover mover;
        private Vector3 initPos;
        private IEnumerator suspiciousCoroutine;

        public delegate void OnSuspicousFinishedDelegate();
        public event OnSuspicousFinishedDelegate OnSuspicousFinishedEvent;

        private void Awake()
        {
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            player = playerGameObject.GetComponent<Player>();
            enemy = GetComponent<Enemy>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
        }

        private void Start()
        {
            initPos = transform.position;
            
        }

        private void DeadBehaviour()
        {
            fighter.CancelAttack();
            mover.DisableMover();
        }

        private void PatrolBheaviour()
        {
            fighter.CancelAttack();
            mover.MoveTo(initPos, 0);
        }
        private void AttackPlayerBehaviour()
        {
            fighter.Attack(player);
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
                    PatrolBheaviour();
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

            CancelPreviousCoroutine();

            suspiciousCoroutine = ActivateSuspicousBehaviour();
            StartCoroutine(suspiciousCoroutine);
        }

        private void CancelPreviousCoroutine()
        {
            if (suspiciousCoroutine != null) StopCoroutine(suspiciousCoroutine);
        }
    }

}
