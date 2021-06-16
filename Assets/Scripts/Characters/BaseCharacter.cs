using RPG.Combat;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;


namespace RPG.Characters
{
    public abstract class BaseCharacter : MonoBehaviour, ISaveable
    {
        /// <summary>
        /// TOOD consider delete healthPoint. This variable is simply for debug purpose to see if changing health takes effect in game. In practice it has no effect on the game.
        /// Need to see how to expose the Health system to the inspector
        /// </summary>
        [SerializeField] protected float healthPoints = 100f;
        
        [SerializeField] protected float runningSpeed = 4f;

        public float RunningSpeed => runningSpeed;
        protected Health healthSystem { get; private set; }
        private Animator animator;
        private Collider characterCollider;
        private IProvideStats stats;

        protected CharacterType charType;

        public CharacterType CharType => charType;

        public bool IsAlive { get; private set; } = true;

        protected abstract Collider GetCharacterCollider();
        protected abstract void SetCharacterType();

        private void Awake()
        {
            animator = GetComponent<Animator>();
            stats = GetComponent<IProvideStats>();
            healthPoints = stats.ProvideHealth();
            healthSystem = Health.CreateHealth(stats.ProvideHealth());
            characterCollider = GetCharacterCollider();    
            SetCharacterType();
        }

        public void TakeDamage(float damage)
        {
            print(healthSystem.HealthPoints);
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

        public object CaptureState()
        {
            return healthSystem.HealthPoints;
        }

        public void RestoreState(object state)
        {
            float savedHealth = (float)state;
            TakeDamage(healthSystem.HealthPoints - savedHealth);
        }

        public enum CharacterType
        {
            NONE,
            Player,
            Enemy
        }
    }
}