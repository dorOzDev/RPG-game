using RPG.Combat;
using RPG.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Control
{
    public abstract class BaseController : MonoBehaviour
    {
        public abstract void OnStateChanged(Enum state);
    }
}
