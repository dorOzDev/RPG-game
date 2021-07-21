using RPG.Characters;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class LevelDisplay : BaseDisplay
    {
        // Start is called before the first frame update
        private Player player;
        private BaseStats playerBaseStats;

        protected override void Awake()
        {
            base.Awake();
            player = FindObjectOfType<Player>();
            playerBaseStats = player.GetComponentInParent<BaseStats>();
        }

        private void OnEnable()
        {
            BaseStats.OnLevelUpEvent += OnLevelUp;
        }

        private void OnDisable()
        {
            BaseStats.OnLevelUpEvent -= OnLevelUp;
        }

        void Start()
        {
            SetLevel();
        }

        private void SetLevel()
        {
            if (uiText != null)
            {
                uiText.text = playerBaseStats.Level + " ";
            }
        }

        void OnLevelUp()
        {
            SetLevel();
        }
    }

}
