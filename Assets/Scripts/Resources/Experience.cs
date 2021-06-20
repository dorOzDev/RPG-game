using RPG.Characters;
using RPG.Saving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Resources
{
    class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float expirencePoints = 0;

        public delegate void OnExperienceGainedDelegate(float experience);
        public static event OnExperienceGainedDelegate OnExperienceGainedEvent;

        private void OnEnable()
        {
            BaseCharacter.OnRewardExperienceEvent += GainExperience;
        }

        private void OnDisable()
        {
            BaseCharacter.OnRewardExperienceEvent -= GainExperience;
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
