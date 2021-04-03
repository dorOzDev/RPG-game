using UnityEngine;

namespace RPG.Combat
{
    public class CombatTarget : MonoBehaviour, IDamageAble
    {
        [SerializeField] private float initHealthPoints = 100f;
        private Health healthSystem;

        private void Awake()
        {
            healthSystem = Health.CreateHealth(initHealthPoints, this.gameObject);         
        }
        public void TakeDamage(float damage)
        {
            healthSystem.TakeDamage(damage);
        }
    }
}
