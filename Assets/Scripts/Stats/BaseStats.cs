using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;


namespace RPG.Stats
{
   public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        //[SerializeField] private int levelSystem = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression = null;

        private LevelSystem levelSystem;
        public delegate void OnLevelUpDelegate();

        public static event OnLevelUpDelegate OnLevelUpEvent;
        public int Level => levelSystem.Level;

        private void Awake()
        {
            levelSystem = GetComponent<LevelSystem>();
        }

        private void OnEnable()
        {
            if(characterClass == CharacterClass.PLAYER)
            {
                Experience.OnExperienceGainedEvent += OnExperienceGained;
            }
        }

        private void OnDisable()
        {
            if(characterClass == CharacterClass.PLAYER)
            {
                Experience.OnExperienceGainedEvent -= OnExperienceGained;
            }
        }
        // This method can only be invoked by the OnExperienceDelegate which is attached
        // only the the player gameobject, hence no other game objects such as enemy or w.e can
        // trigger this method.
        private void OnExperienceGained(float currExperience)
        {
            if (currExperience != 0)
            {
                UpdateLevel(currExperience);
            }
        }

        private void UpdateLevel(float currExperience)
        {
            int maxSupportedLevel = progression.GetMaxLevelSupported(Stat.ExperienceToNextLevel, CharacterClass.PLAYER);
            if (maxSupportedLevel <= Level)
            {
                // No action required
                return;
            }

            float expToNextLevel = progression.GetStat(Stat.ExperienceToNextLevel, CharacterClass.PLAYER, Level);

            if(expToNextLevel <= currExperience)
            {
                levelSystem.LevelUp();
                OnLevelUpEvent?.Invoke();
            }
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, Level);
        }
    }
}
