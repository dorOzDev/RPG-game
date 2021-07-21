using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    class ExperienceDisplay : BaseDisplay
    {
        private void OnEnable()
        {
            Experience.OnExperienceGainedEvent += OnExperienceChanged;
            BaseStats.OnLevelUpEvent += OnLevelUp;
        }

        private void OnLevelUp()
        {
            OnExperienceChanged(0);
        }

        private void OnDisable()
        {
            Experience.OnExperienceGainedEvent -= OnExperienceChanged;
            BaseStats.OnLevelUpEvent += OnLevelUp;
        }

        private void OnExperienceChanged(float experience)
        {
            if(uiText != null)
            {
                uiText.text = experience + "";
            }
        }
        
    }
}
