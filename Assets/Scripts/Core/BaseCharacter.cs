using RPG.Combat;
using RPG.Movement;
using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public abstract class BaseCharacter : MonoBehaviour, ISaveable
    {
        [SerializeField] protected float healthPoints = 100f;
        
        [SerializeField] protected float runningSpeed = 4f;

        public float RunningSpeed => runningSpeed;
        protected Health healthSystem { get; private set; }
        private Animator animator;
        private Collider characterCollider;

        public bool IsAlive { get; private set; } = true;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            healthSystem = Health.CreateHealth(healthPoints);
            characterCollider = GetCharacterCollider();
        }

        public void TakeDamage(float damage)
        {
            healthSystem.TakeDamage(damage);

            UpdateInspectorHealthPoints();

            if(healthSystem.HealthPoints == 0 && IsAlive)
            {
                TriggerDeath();
            }
        }

        private void UpdateInspectorHealthPoints()
        {
            healthPoints = healthSystem.HealthPoints;
        }

        protected virtual void TriggerDeath()
        {
            animator.SetTrigger("die");
            characterCollider.enabled = false;
            IsAlive = false;
        }

        protected abstract Collider GetCharacterCollider();

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            float savedHealth = (float)state;
            TakeDamage(healthPoints - savedHealth);
        }
    }
}