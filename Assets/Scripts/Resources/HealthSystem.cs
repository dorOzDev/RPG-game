using RPG.Saving;
using System;
using System.Runtime.InteropServices;
using UnityEngine;


namespace RPG.Resources
{ 
    public class HealthSystem : ScriptableObject
    {
        private float m_healthPoints = -1;
        public float HealthPoints => m_healthPoints;


        public static HealthSystem CreateHealth()
        {
            HealthSystem healthSystemScript = CreateInstance<HealthSystem>();
            return healthSystemScript;
        }

        public void SetHealth(float health)
        {
            if (m_healthPoints == -1)
            {
                m_healthPoints = health;
            }
        }

        public void TakeDamage(float damage)
        {
            m_healthPoints = Mathf.Max(m_healthPoints - damage, 0);
        }
    }
}
