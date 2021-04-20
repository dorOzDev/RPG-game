using RPG.Core;
using UnityEditor;
using UnityEngine;

namespace RPG.Characters
{
    public class Player : BaseCharacter
    {
        protected override Collider GetCharacterCollider()
        {
            return GetComponent<CapsuleCollider>();
        }
    }
}


