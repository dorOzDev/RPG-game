using RPG.Control;
using RPG.Core;
using System;
using UnityEngine;

namespace RPG.Characters
{
    public class Enemy : BaseCharacter
    {
        [SerializeField] private float chaseDistance = 5f;
        public float ChaseDistance => chaseDistance;

        [SerializeField] private float suspiciousDuration = 2f;
        public float SuspiciousDuration => suspiciousDuration;

        protected override Collider GetCharacterCollider()
        {
            return GetComponent<CapsuleCollider>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(gameObject.transform.position, chaseDistance);
        }
    }

}
