using RPG.Combat;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField] private float initialHealthPoints = 100f;
        
        [SerializeField]
        protected float runningSpeed = 4f;

        public float RunningSpeed => runningSpeed;
        protected Health healthSystem { get; private set; }

        private Animator animator;
        private Collider characterCollider;

        public bool IsAlive { get; private set; } = true;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Awake()
        {
            healthSystem = Health.CreateHealth(initialHealthPoints);
            characterCollider = GetCharacterCollider();
        }

        public void TakeDamage(float damage)
        {
            healthSystem.TakeDamage(damage);
            if(healthSystem.HealthPoints == 0 && IsAlive)
            {
                TriggerDeath();
            }
        }

         protected virtual void TriggerDeath()
        {
            animator.SetTrigger("die");
            characterCollider.enabled = false;
            IsAlive = false;
        }

        protected abstract Collider GetCharacterCollider();
    }
}