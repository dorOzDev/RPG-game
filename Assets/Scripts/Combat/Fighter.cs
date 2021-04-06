using RPG.Characters;
using RPG.Core;
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

        private BaseCharacter target;
        private Mover mover;
        private Animator animator;
        private bool shouldAttack = false;

        [SerializeField] private float damage = 3f;

        [SerializeField] private float weaponRange = 1f;

        [SerializeField] private float timeBetweenAttacks = 3f;

        private bool isInRest = false;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private bool CanAttack()
        {
            return shouldAttack && target != null && target.IsAlive;
        }
        private void Update()
        {   
            if (CanAttack())
            {
                mover.MoveTo(target.transform.position, weaponRange);

                transform.LookAt(target.transform);

                StartCoroutine(TriggerAttackAnimation());
            }
        }

        public void Attack(BaseCharacter target)
        {
            shouldAttack = true;
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

        private IEnumerator TriggerAttackAnimation()
        {
            while (!IsInAttackRange())
            {
                yield return null;
            }

            if (!isInRest) 
            {
                TriggerAttack();
            }
        }

        private bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger(STOP_ATTACK);
            animator.SetTrigger(ATTACK);

            isInRest = true;
            StartCoroutine(StartRestBetweenAttacksCountDown());
        }

        public void CancelAttack()
        {
            shouldAttack = false;
            animator.ResetTrigger(ATTACK);
            animator.SetTrigger(STOP_ATTACK);
        }
        // Animation event(is called once the animator SetsTrigget of "attack"
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(damage);

        }
    }
}
