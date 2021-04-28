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
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Weapon defaultWeapon = null;
        private Weapon currWeapon = null;


        private bool isInRest = false;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            if (CanAttack())
            {
                mover.MoveTo(target.transform.position, attacker.RunningSpeed, currWeapon.WeaponRange);

                transform.LookAt(target.transform);

                StartCoroutine(TriggerAttackAnimation());
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currWeapon = weapon;
            if (weapon != null)
            {
                weapon.Spawn(rightHandTransform, leftHandTransform, animator);
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
            return Vector3.Distance(transform.position, target.transform.position) <= currWeapon.WeaponRange;
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
        // Animation event(is called once the animator SetsTrigget of "attack")
        void Hit()
        {
            if (target == null) return;

            if (currWeapon is ProjectileWeapon) ShootProjectile();

            target.TakeDamage(currWeapon.Damage);
        }

        void Shoot()
        {
            Hit();
        }

        private void ShootProjectile()
        {
            ProjectileWeapon projectileWeapon = currWeapon as ProjectileWeapon;

            Projectile projectile = projectileWeapon.SpawnProjectile(rightHandTransform);

            projectile.ShootProjectile(target.transform);
        }
    }
}
