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
        [SerializeField] private float initialHealthPoints = 100f;
        
        [SerializeField]
        protected float runningSpeed = 4f;

        public float RunningSpeed => runningSpeed;
        protected Health healthSystem { get; private set; }

        public float currentHealthPoints => healthSystem.HealthPoints;

        private Animator animator;
        private Collider characterCollider;

        public bool IsAlive { get; private set; } = true;


        private void Start()
        {    
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
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

        public object CaptureState()
        {
            return currentHealthPoints;
        }

        public void RestoreState(object state)
        {
            float savedHealth = (float)state;
            TakeDamage(currentHealthPoints - savedHealth);
            print(currentHealthPoints);
        }
    }
}