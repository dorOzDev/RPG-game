using System;
using RPG.Core;
using RPG.Stats;
using UnityEditor;
using UnityEngine;

namespace RPG.Characters
{
    public class Player : BaseCharacter
    {
        [Range(0, 100)] 
        [SerializeField] private float regenartionHealthOnLevelUp = 75f;
        private void OnEnable()
        {
            BaseStats.OnLevelUpEvent += OnLevelUp;
        }

        private void OnDisable()
        {
            BaseStats.OnLevelUpEvent -= OnLevelUp;
        }

        private void OnLevelUp()
        {
            float totalHp = stats.GetStat(Stat.Health);
            float amountToHeal = (totalHp * regenartionHealthOnLevelUp / 100) - HealthSystemSystem.HealthPoints;
            HealDamage(amountToHeal);
        }

        protected override Collider GetCharacterCollider()
        {
            return GetComponent<CapsuleCollider>();
        }

        protected override void SetCharacterType()
        {
            this.charType = CharacterType.Player;
        }
    }
}


