using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats
{
    class LevelSystem : MonoBehaviour, ISaveable
    {
        [SerializeField] private int level = 1;

        public int Level => level;

        public object CaptureState()
        {
            return level;
        }

        public void LevelUp()
        {
            level++;
        }

        public void RestoreState(object state)
        {
            level = (int)state;
        }
    }
}
