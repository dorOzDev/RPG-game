using RPG.Characters;
using RPG.Movement;
using System;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    
    public class Fighter : MonoBehaviour
    {
        private const string ATTACK = "attack";
        private const string STOP_ATTACK = "stopAttack";

        private Enemy target;
        private Mover mover;
        private Animator animator;

        [SerializeField] private float damage = 3f;

        [SerializeField] private float weaponRange = 2f;

        [SerializeField] private float timeBetweenAttacks = 3f;

        private bool isInRest = false;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private bool CanAttack()
        {
            return target != null && target.IsAlive;
        }
        private void Update()
        {   
            if (CanAttack())
            {
                mover.MoveTo(target.transform.position, weaponRange);

                transform.LookAt(target.transform);

                StartCoroutine(TriggerAttackAnimation(target.transform.position));
            }
        }

        public void Attack(Enemy target)
        {
            this.target = target;
        }

        private IEnumerator StartRestBetweenAttacksCountDown()
        {
            float totalTime = 0;
            while(isInRest && totalTime <= timeBetweenAttacks)
            {
                totalTime += Time.deltaTime;
                yield return null;
            }

            isInRest = false;
        }

        private IEnumerator TriggerAttackAnimation(Vector3 enemyPosition)
        {
            while(Vector3.Distance(transform.position, enemyPosition) > weaponRange)
            {
                yield return null;
            }

            if (!isInRest) 
            {
                TriggerAttack();
            }
        }

        private void StopAttack()
        {
            animator.ResetTrigger(ATTACK);
            animator.SetTrigger(STOP_ATTACK);
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger(STOP_ATTACK);
            animator.SetTrigger(ATTACK);

            isInRest = true;
            StartCoroutine(StartRestBetweenAttacksCountDown());
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }
        // Animation event(is called once the animator SetsTrigget of "attack"
        void Hit()
        {
            if (target == null) return;

            target.TakeDamage(damage);
        }
    }
}
