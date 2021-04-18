using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Saving
{
    class SavingSystemComponent : MonoBehaviour
    {

        [SerializeField] private SavingSystem savingSystemInstace;

        public SavingSystem SavingSystemInstance => savingSystemInstace;
    }
}
