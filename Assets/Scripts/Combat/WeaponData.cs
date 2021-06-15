using System;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon instance", menuName = "ScriptableObjects/Create weapon instance")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController weaponOverrideController = null;
        [Tooltip("Leave empty if left hand weapon is unneeded")]
        [SerializeField] private GameObject leftHandWeaponPrefab;
        [Tooltip("Leave empty if right hand weapon is unneeded")]
        [SerializeField] private GameObject rightHandWeaponPrefab;
        [SerializeField] private float damage = 3f;
        [SerializeField] private float weaponRange = 1f;
        /*        [SerializeField] private WeaponHand hand;

                public WeaponHand Hand => hand;*/

        public float Damage => damage;
        public float WeaponRange => weaponRange;

        public IList<GameObject> Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            IList<GameObject> equippedWeaponList = new List<GameObject>();
            equippedWeaponList.Add(SpawnOneHandWeapon(leftHand, leftHandWeaponPrefab));
            equippedWeaponList.Add(SpawnOneHandWeapon(rightHand, rightHandWeaponPrefab));

            if (weaponOverrideController != null)
            {
                animator.runtimeAnimatorController = weaponOverrideController;
            }
            else
            {
                var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
                if (overrideController != null)
                {
                    animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
                }
            }

            return equippedWeaponList;
        }



        private GameObject SpawnOneHandWeapon(Transform hand, GameObject weaponPrefab)
        {
            GameObject instantiatedWeapon;
            if (weaponPrefab == null)
            {
                instantiatedWeapon = new GameObject();
                instantiatedWeapon.transform.parent = hand;
            }
            else
            {
                instantiatedWeapon = Instantiate(weaponPrefab, hand);
            }

            return instantiatedWeapon;
        }
    }
}
