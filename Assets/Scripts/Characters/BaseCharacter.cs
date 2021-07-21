using RPG.Resources;
using RPG.Saving;
using RPG.Stats;
using System;
using System.Data;
using UnityEngine;


namespace RPG.Characters
{
    public abstract class BaseCharacter : MonoBehaviour, ISaveable
    {
        [SerializeField] protected float runningSpeed = 4f;

        public float RunningSpeed => runningSpeed;
        protected HealthSystem HealthSystemSystem { get; private set; }
        private Animator animator;
        private Collider characterCollider;
        private BaseStats stats;

        protected CharacterType charType;

        public CharacterType CharType => charType;

        public bool IsAlive { get; private set; } = true;

        protected abstract Collider GetCharacterCollider();
        protected abstract void SetCharacterType();

        /// <summary>
        /// For debug purpose I provide an event for when the health point of any character is effected
        /// </summary>
        public delegate void OnDamageTakenDelegate(BaseCharacter baseCharacter, float percentage);
        public static event OnDamageTakenDelegate OnDamageTakenEvent;

        public delegate void OnRewardExperienceDelegate(BaseCharacter character, float experience);
        public static event OnRewardExperienceDelegate OnRewardExperienceEvent;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            stats = GetComponent<BaseStats>();
            HealthSystemSystem = HealthSystem.CreateHealth();
            characterCollider = GetCharacterCollider();    
            SetCharacterType();
        }

        private void Start()
        {
            float initHp = stats.GetStat(Stat.Health);
            HealthSystemSystem.SetHealth(initHp);
        }

        public void TakeDamage(float damage)
        {
            HealthSystemSystem.TakeDamage(damage);
            OnDamageTakenEvent?.Invoke(this, GetPercentageHealth());

            if(HealthSystemSystem.HealthPoints == 0 && IsAlive)
            {
                TriggerDeath();
            }
        }

        private float GetPercentageHealth()
        {
            return 100 * (HealthSystemSystem.HealthPoints / stats.GetStat(Stat.Health));
        }

        protected virtual void TriggerDeath()
        {
            OnRewardExperienceEvent?.Invoke(this, stats.GetStat(Stat.ExperienceReward));
            animator.SetTrigger("die");
            characterCollider.enabled = false;
            IsAlive = false;
        }

        public object CaptureState()
        {
            return HealthSystemSystem.HealthPoints;
        }

        public void RestoreState(object state)
        {
            float savedHealth = (float)state;
            TakeDamage(HealthSystemSystem.HealthPoints - savedHealth);
        }

        public enum CharacterType
        {
            NONE,
            Player,
            Enemy
        }
    }
}