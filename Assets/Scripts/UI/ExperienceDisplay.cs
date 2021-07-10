using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
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
