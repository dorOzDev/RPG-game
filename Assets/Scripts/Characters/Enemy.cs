using RPG.Core;
using UnityEngine;

namespace RPG.Characters
{
    public class Enemy : BaseCharacter
    {
        protected override Collider GetCharacterCollider()
        {
            return GetComponent<CapsuleCollider>();
        }
    }
}
