using RPG.Characters;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    class HealthDisplay : MonoBehaviour
    {
        private Text healthValueText;

        private void Awake()
        {
            healthValueText = GetComponent<Text>();
        }

        private void OnEnable()
        {
            BaseCharacter.OnDamageTakenEvent += OnHealthChanged;
        }

        private void OnDisable()
        {
            BaseCharacter.OnDamageTakenEvent -= OnHealthChanged;
        }

        public void OnHealthChanged(BaseCharacter character, float percentage)
        {
            if(character.CharType == BaseCharacter.CharacterType.Player)
            {
                int roundedPercentage = (int)Math.Ceiling(percentage);
                healthValueText.text = roundedPercentage + "%";
            }
        }
    }
}
