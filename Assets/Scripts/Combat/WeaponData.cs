using System;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon instance", menuName = "ScriptableObjects/Create weapon instance")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController weaponOverrideController = null;
        [SerializeField] private GameObject weaponPrefab = null;
        [SerializeField] private float damage = 3f;
        [SerializeField] private float weaponRange = 1f;
        [SerializeField] private WeaponHand hand;

        
        public float Damage => damage;
        public float WeaponRange => weaponRange;

        public virtual IList<GameObject> Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            IList<GameObject> equippedWeaponList = new List<GameObject>();
            if(BareHandWeapon())
            {
                // If the weapon is bare handed than both hands are the weapons.
                equippedWeaponList.Add(rightHand.gameObject);
                equippedWeaponList.Add(leftHand.gameObject);
            }
            else
            {
                equippedWeaponList.Add(Instantiate(weaponPrefab, GetCorrectHandSpawn(rightHand, leftHand)));
            }
            if (weaponOverrideController != null)
            {
                animator.runtimeAnimatorController = weaponOverrideController;
            }

            return equippedWeaponList;
        }

        private bool BareHandWeapon()
        {
            return weaponPrefab == null;
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
        LEFT,
        TWO_HANDED
    }
}
