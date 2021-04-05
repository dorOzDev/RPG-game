using RPG.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Characters
{
    class Player : BaseCharacter
    {
        protected override Collider GetCharacterCollider()
        {
            return null;
        }
    }
}
