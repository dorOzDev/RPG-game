using RPG.Characters;
using System;

namespace RPG.UI
{
    class HealthDisplay : BaseDisplay
    {

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
                uiText.text = roundedPercentage + "%";
            }
        }
    }
}
