using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Stats
{
   public class BaseStats : MonoBehaviour, IProvideStats
    {
        [Range(1, 99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression = null;

        public float ProvideExperienceReward()
        {
            return progression.GetStat(Stat.ExperienceReward, characterClass, startingLevel);
        }

        public float ProvideInitialHealth()
        {
            return progression.GetStat(Stat.Health, characterClass, startingLevel);
        }
    }
}
