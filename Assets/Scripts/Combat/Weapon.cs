using System;
using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon instance", menuName = "ScriptableObjects/Create weapon instance")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController weaponOverrideController = null;
        [SerializeField] private GameObject weaponPrefab = null;
        [SerializeField] private float damage = 3f;
        [SerializeField] private float weaponRange = 1f;
        [SerializeField] private WeaponHand hand;

        

        public float Damage => damage;
        public float WeaponRange => weaponRange;

        public virtual GameObject Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            GameObject equippedWeapon = null;
            if (weaponPrefab != null)
            {
                equippedWeapon = Instantiate(weaponPrefab, GetCorrectHandSpawn(rightHand,leftHand));
            }
            if (weaponOverrideController != null)
            {
                animator.runtimeAnimatorController = weaponOverrideController;
            }

            return equippedWeapon;
        }

        protected Transform GetCorrectHandSpawn(Transform rightHand, Transform leftHand)
        {
            Transform handTransform = rightHand;

            if (hand == WeaponHand.LEFT) handTransform = leftHand;

            return handTransform;
        }
    }

    enum WeaponHand
    {
        RIGHT,
        LEFT
    }
}
