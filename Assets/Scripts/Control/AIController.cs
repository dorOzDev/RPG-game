using RPG.Characters;
using RPG.Combat;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspiciousDuration = 2f;
        private GameObject playerGameObject;
        private Player player;
        private Enemy enemy;
        private Fighter fighter;
        private Mover mover;
        private Vector3 initPos;

        private void Start()
        {
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            player = playerGameObject.GetComponent<Player>();
            enemy = GetComponent<Enemy>();
            initPos = transform.position;
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
        }
        private void Update()
        {
            if (!enemy.IsAlive)
            {
                fighter.CancelAttack();
                return;
            }
            AttackPlayer();
        }

        private void AttackPlayer()
        {
            if (IsPlayerInRange() && player.IsAlive)
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.CancelAttack();
                mover.MoveTo(initPos, 0);
                //StartCoroutine(WaitInLocation());
            }
        }

        IEnumerator WaitInLocation()
        {
            float totalTime = 0;
            while (totalTime <= suspiciousDuration)
            {
                totalTime += Time.deltaTime;
                yield return null;
            }
        }

        private bool IsPlayerInRange()
        {
            return Vector3.Distance(transform.position, playerGameObject.transform.position) <= chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(gameObject.transform.position, chaseDistance);
        }
    }

}
