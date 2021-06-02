using RPG.Characters;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RPG.Characters.BaseCharacter;

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
        [SerializeField] WeaponData defaultWeapon = null;
        private WeaponData currWeapon = null;

        private IList<GameObject> equippedWeapons = null;

        private bool isInRest = false;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            EquipWeapon(defaultWeapon);
            SetWeaponActivity(false);
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

        public void EquipWeapon(WeaponData weapon)
        {
            DestroyOldWeapon();
            currWeapon = weapon;
            if (weapon != null)
            {
                equippedWeapons = weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            }
        }

        private void DestroyOldWeapon()
        {
            if (equippedWeapons == null || equippedWeapons.Count == 0 || currWeapon == defaultWeapon) return;
            foreach(GameObject weapon in equippedWeapons)
            {
                Destroy(weapon);
            }
        }

        private bool CanAttack()
        {
            return shouldAttack && target != null && target.IsAlive;
        }

        public void Attack(BaseCharacter target, BaseCharacter attacker)
        {
            SetWeaponActivity(true);
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

        private void SetWeaponActivity(bool actitvity)
        {
            if (equippedWeapons == null) return;

            foreach(GameObject equippedWeapon in equippedWeapons)
            {
                equippedWeapon.GetComponentInParent<Collider>().enabled = actitvity;
            }
        }

        public void CancelAttack()
        {
            SetWeaponActivity(false);
            shouldAttack = false;
            animator.ResetTrigger(ATTACK);
            animator.SetTrigger(STOP_ATTACK);
        }
        // Animation event(is called once the animator SetsTrigget of "attack")
        void Hit()
        {
            if (target == null) return;

            if (currWeapon is ProjectileWeaponData) ShootProjectile();
        }

        void Shoot()
        {
            Hit();
        }

        private void ShootProjectile()
        {
            ProjectileWeaponData projectileWeapon = currWeapon as ProjectileWeaponData;

            Projectile projectile = projectileWeapon.SpawnProjectile(rightHandTransform);

            projectile.Attack(target);
        }
    }
}
