using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon instance", menuName = "ScriptableObjects/Create weapon instance")]
    class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController weaponOverrideController = null;
        [SerializeField] private GameObject weaponPrefab = null;
        [SerializeField] private float damage = 3f;
        [SerializeField] private float weaponRange = 1f;

        public float Damage => damage;
        public float WeaponRange => weaponRange;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if(weaponPrefab != null)
            {
                Instantiate(weaponPrefab, handTransform);
            }
            if(weaponOverrideController != null)
            {
                animator.runtimeAnimatorController = weaponOverrideController;
            }

        }

    }
}
