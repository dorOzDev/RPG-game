using Assets.Scripts.Combat;
using RPG.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RPG.Characters.BaseCharacter;

namespace RPG.Combat
{
    public class Projectile : Weapon
    {
        // Start is called before the first frame update
        [SerializeField] float distanceThreshHold = 1f;
        [SerializeField] float projectileSpeed = 1f;
        [SerializeField] float shootDistance = 10f;

        private BaseCharacter target;

        public void Attack(BaseCharacter target)
        {
            transform.LookAt(target.transform);
            this.target = target;
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
            if(hitCharacter != null)
            {
                
                if(hitCharacter.CharType == target.CharType)
                {
                    target.TakeDamage(weaponData.Damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
