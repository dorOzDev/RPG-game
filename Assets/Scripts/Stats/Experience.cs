using RPG.Characters;
using RPG.Saving;
using System;
using UnityEngine;

namespace RPG.Stats
{
    class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] private float expirencePoints = 0;

        public float ExperiencePoints => expirencePoints;

        public delegate void OnExperienceGainedDelegate(float experience);
        public static event OnExperienceGainedDelegate OnExperienceGainedEvent;

        private void OnEnable()
        {
            BaseCharacter.OnRewardExperienceEvent += GainExperience;
            BaseStats.OnLevelUpEvent += ResetExp;
        }

        private void OnDisable()
        {
            BaseCharacter.OnRewardExperienceEvent -= GainExperience;
        }

        private void ResetExp()
        {
            expirencePoints = 0;
        }

        public void GainExperience(BaseCharacter baseCharacter,float experience)
        {
            if(baseCharacter.CharType == BaseCharacter.CharacterType.Enemy)
            {
                expirencePoints += experience;
                OnExperienceGainedEvent?.Invoke(expirencePoints);
            }     
        }

        public object CaptureState()
        {
            return expirencePoints;
        }

        public void RestoreState(object state)
        {
            expirencePoints = (float)state;
        }
    }
}
