using RPG.Saving;
using System;
using System.Runtime.InteropServices;
using UnityEngine;


namespace RPG.Combat
{ 
    public class Health : ScriptableObject
    {
        private float m_healthPoints;
        public float HealthPoints => m_healthPoints;


        public static Health CreateHealth(float health)
        {
            Health healthScript = CreateInstance<Health>();
            healthScript.Init(health);
            return healthScript;
        }

        private void Init(float health)
        {
            SetHealth(health);
        }

        private void SetHealth(float health)
        {
            m_healthPoints = health;
        }

        public void TakeDamage(float damage)
        {
            m_healthPoints = Mathf.Max(m_healthPoints - damage, 0);
        }
    }
}
