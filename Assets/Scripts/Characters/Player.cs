using RPG.Core;
using UnityEditor;
using UnityEngine;

namespace RPG.Characters
{
    class Player : BaseCharacter
    {
        public float Health => healthSystem.HealthPoints;
        protected override Collider GetCharacterCollider()
        {
            return GetComponent<CapsuleCollider>();
        }
    }
}


