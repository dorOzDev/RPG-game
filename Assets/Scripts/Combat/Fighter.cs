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
        private BaseCharacter attacker;
        private Mover mover;
        private Animator animator;
        private bool shouldAttack = false;

        [SerializeField] private float timeBetweenAttacks = 3f;  
        [SerializeField] Transform handTransform;
        [SerializeField] Weapon weapon = null;


        private bool isInRest = false;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            SpawnWeapon();
        }

        private void Update()
        {
            if (CanAttack())
            {
                mover.MoveTo(target.transform.position, attacker.RunningSpeed, weapon.WeaponRange);

                transform.LookAt(target.transform);

                StartCoroutine(TriggerAttackAnimation());
            }
        }

        private void SpawnWeapon()
        {
            if(weapon != null)
            {
                weapon.Spawn(handTransform, animator);
            }
        }

        private bool CanAttack()
        {
            return shouldAttack && target != null && target.IsAlive;
        }

        public void Attack(BaseCharacter target, BaseCharacter attacker)
        {
            shouldAttack = true;
            this.target = target;
            this.attacker = attacker;
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
            return Vector3.Distance(transform.position, target.transform.position) <= weapon.WeaponRange;
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
            target.TakeDamage(weapon.Damage);

        }
    }
}
