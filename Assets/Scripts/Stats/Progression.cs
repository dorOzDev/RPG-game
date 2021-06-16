using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Create progression instance")]
    class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] characterClasses;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            ProgressionCharacterClass progressionCharacter = GetCharacter(characterClass);

            if(progressionCharacter == null)
            {
                Debug.LogError("Charactr: " + characterClass + "was not found in progrssion, are you sure this character was setup?");
                return 1;
            }

            return progressionCharacter.GetHealthByLevel(level);
        }

        private ProgressionCharacterClass GetCharacter(CharacterClass characterClass)
        {
            foreach(ProgressionCharacterClass progressionCharacter in characterClasses)
            {
                if(progressionCharacter.CharacterClass == characterClass)
                {
                    return progressionCharacter;
                }
            }
            // If none found return null;
            return null;
        }

        public float GetDamage(CharacterClass characterClass)
        {
            ProgressionCharacterClass progressionCharacter = characterClasses[(int)characterClass];

            return progressionCharacter.Damage;
        }

        [Serializable]
        class ProgressionCharacterClass
        {
            [Tooltip("The health is array to satisfy health changes by level progression")]
            [SerializeField] private float[] health = new float[1];
            [SerializeField] private float damage = 3;
            [SerializeField] private CharacterClass characterClass;

            public CharacterClass CharacterClass => characterClass;
            public float Damage => damage;

            public float GetHealthByLevel(int level)
            {
                if(health.Length == 0)
                {
                    Debug.LogError("Health was not set for class: " + characterClass);
                    return 1;
                }
                int healthForLevel = Math.Max(Math.Min(level - 1, health.Length - 1), 0);
                return health[healthForLevel];
            }
        }
    }
}
