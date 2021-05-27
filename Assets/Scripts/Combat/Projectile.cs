using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] float distanceThreshHold = 1f;
        [SerializeField] float projectileSpeed = 1f;

        public void ShootProjectile(Transform target)
        {
            transform.LookAt(target);
            StartCoroutine(ShootProjectileOverSpeed(target));
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

        private void OnTriggerEnter(Collider other)
        {
            //print("Collision with: " + other.gameObject.name);
        }
    }
}
