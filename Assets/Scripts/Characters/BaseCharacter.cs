using RPG.Combat;
using RPG.Saving;
using UnityEngine;


namespace RPG.Characters
{
    public abstract class BaseCharacter : MonoBehaviour, ISaveable
    {
        [SerializeField] protected float healthPoints = 100f;
        
        [SerializeField] protected float runningSpeed = 4f;

        public float RunningSpeed => runningSpeed;
        protected Health healthSystem { get; private set; }
        private Animator animator;
        private Collider characterCollider;

        protected CharacterType charType;

        public CharacterType CharType => charType;

        public bool IsAlive { get; private set; } = true;

        protected abstract Collider GetCharacterCollider();
        protected abstract void SetCharacterType();

        private void Awake()
        {
            animator = GetComponent<Animator>();
            healthSystem = Health.CreateHealth(healthPoints);
            characterCollider = GetCharacterCollider();
            SetCharacterType();
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

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            float savedHealth = (float)state;
            TakeDamage(healthPoints - savedHealth);
        }

        private void OnTriggerEnter(Collider other)
        {
            Projectile projectile = other.GetComponentInParent<Projectile>();
            if(projectile != null)
            {
               
            }
        }

        public enum CharacterType
        {
            NONE,
            Player,
            Enemy
        }
    }
}