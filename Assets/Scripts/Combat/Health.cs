using System;
using UnityEngine;


namespace RPG.Combat
{ 
    public class Health : ScriptableObject
    {
        private float m_healthPoints;
        public float HealthPoints => m_healthPoints;

        private GameObject gameObject;

        public static Health CreateHealth(float health, GameObject gameObject)
        {
            Health healthScript = ScriptableObject.CreateInstance<Health>();
            healthScript.Init(health, gameObject);
            return healthScript;
        }

        private void Init(float health, GameObject gameObject)
        {
            this.gameObject = gameObject;
            SetHealth(health);
        }

        private void SetHealth(float health)
        {
            m_healthPoints = health;
        }

        public void TakeDamage(float damage)
        {
            m_healthPoints = Mathf.Max(m_healthPoints - damage, 0);
            if(m_healthPoints == 0)
            {
                // Raise event this object should be killed.
            }
        }
    }
}
