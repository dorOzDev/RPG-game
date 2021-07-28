using Assets.Scripts.Combat;
using RPG.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;
using static RPG.Characters.BaseCharacter;

namespace RPG.Combat
{
    public class Projectile : Weapon
    {
        [SerializeField] float distanceThreshHold = 1f;
        [SerializeField] float projectileSpeed = 1f;
        [SerializeField] float shootDistance = 10f;
        [SerializeField] GameObject impactEffectPrefab;

        private BaseCharacter target;
        private BaseCharacter attacker;

        public void Attack(BaseCharacter target, BaseCharacter attacker)
        {
            transform.LookAt(target.transform);
            this.target = target;
            this.attacker = attacker;
            StartCoroutine(ShootProjectileOverSpeed(target.transform));
        }

        private Vector3 GetAimLocation(Transform target)
        {
            CapsuleCollider collider = target.GetComponent<CapsuleCollider>();

            if (collider == null) return target.position;

            return target.position + Vector3.up * collider.height / 2;
        }

        IEnumerator ShootProjectileOverSpeed(Transform target)
        {
            Vector3 aimPosition = GetAimLocation(target);
            
            while(Vector3.Distance(transform.position, aimPosition) > distanceThreshHold) 
            {
                transform.position = Vector3.MoveTowards(transform.position, aimPosition, projectileSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            Destroy(gameObject);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            BaseCharacter hitCharacter = other.GetComponentInParent<BaseCharacter>();
            BaseStats stats = attacker.GetComponent<BaseStats>();
            if(hitCharacter != null)
            {
                if(hitCharacter.CharType == target.CharType)
                {
                    PlayImpactEffect(other.transform);
                    target.TakeDamage(weaponData.Damage);
                    Destroy(gameObject);
                }
            }
        }

        private void PlayImpactEffect(Transform hitTransform)
        {
            if (impactEffectPrefab == null) return;

            Instantiate(impactEffectPrefab, GetAimLocation(hitTransform), transform.rotation);
        }
    }
}
