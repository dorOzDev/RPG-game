using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Create progression instance")]
    class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] characterClasses;

        private Dictionary<CharacterClass, Dictionary<Stat, ProgressionStat>> loopUpTable;

        public const float defaultErrorValue = 1f;

        private void Awake()
        {
            BuildLookpUpTable();
        }

        private void BuildLookpUpTable()
        {
            if (loopUpTable != null) return;

            loopUpTable = new Dictionary<CharacterClass, Dictionary<Stat, ProgressionStat>>();

            foreach (ProgressionCharacterClass progressionCharacter in characterClasses)
            {
                Dictionary<Stat, ProgressionStat> dict = new Dictionary<Stat, ProgressionStat>();

                foreach(ProgressionStat progStat in progressionCharacter.ProgressionStats)
                {
                    dict.Add(progStat.Stat, progStat);
                }

                loopUpTable.Add(progressionCharacter.CharacterClass, dict);
            }
        }
        /// <summary>
        /// Returns the maximum supported Level for a given start of a class
        /// </summary>
        /// <param name="stat">Simple</param>
        /// <param name="characterClass"></param>
        /// <returns></returns>
        public int GetMaxLevelSupported(Stat stat, CharacterClass characterClass)
        {
            BuildLookpUpTable();
            Dictionary<Stat, ProgressionStat> characterDict = loopUpTable[characterClass];
            if (characterDict == null)
            {
                Debug.LogError("Progression stats was not implemented for the class: " + characterClass);
                return 1;
            }

            ProgressionStat progressionStat = characterDict[stat];

            return progressionStat.GetMaxSupportedLevel();
        }

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookpUpTable();

            Dictionary<Stat, ProgressionStat> characterTable = loopUpTable[characterClass];
            if(characterTable == null)
            {
                Debug.LogError("Progression stats was not implemented for the class: " + characterClass);
                return defaultErrorValue;
            }

            ProgressionStat progStat = characterTable[stat];
            return GetStatValue(stat, level, progStat, characterClass);
        }

        // Character class is provided only for debug purpose
        private float GetStatValue(Stat stat, int level, ProgressionStat progStat, CharacterClass characterClass)
        {
            if (progStat == null)
            {
                Debug.Log("No values was set for the character class:" + characterClass + " under the stat: " + stat);
                return defaultErrorValue;
            }
            return progStat.GetValueByLevel(level);
        }
        
        [Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] private CharacterClass characterClass;
            [SerializeField] private ProgressionStat[] progressionStats;
            // Getters
            public ProgressionStat[] ProgressionStats => progressionStats;
            public CharacterClass CharacterClass => characterClass;
        }

        [Serializable]
        class ProgressionStat
        {
            [SerializeField] private Stat stat;
            [SerializeField] private float[] levels;

            public Stat Stat => stat;
            public float[] Levels => levels;

            public float GetValueByLevel(int level)
            {
                if(levels.Length == 0)
                {
                    Debug.LogError("Stat:"+ stat + "Doesn't implement any values");
                    return defaultErrorValue;
                }

                int maxLevel = Math.Max(Math.Min(level - 1, levels.Length - 1), 0);
                return levels[maxLevel];
            }

            public int GetMaxSupportedLevel()
            {
                if (levels.Length == 0)
                {
                    Debug.LogError("Stat:" + stat + "Doesn't implement any values");
                    return 1;
                }
                return levels.Length;
            }
        }
    }
}
