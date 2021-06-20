using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    class ExperienceDisplay : MonoBehaviour
    {
        private Text experienceText;

        private void Awake()
        {
            experienceText = GetComponent<Text>();
        }

        private void OnEnable()
        {
            Experience.OnExperienceGainedEvent += OnExperienceChanged;
        }

        private void OnDisable()
        {
            Experience.OnExperienceGainedEvent -= OnExperienceChanged;
        }

        private void OnExperienceChanged(float experience)
        {
            if(experienceText != null)
            {
                experienceText.text = experience + "";
            }
        }
        
    }
}
