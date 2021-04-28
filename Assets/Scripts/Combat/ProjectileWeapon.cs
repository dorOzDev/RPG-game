using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Projectile Weapon instance", menuName = "ScriptableObjects/Create projectile weapon instance")]
    class ProjectileWeapon : Weapon
    {
        [SerializeField] private GameObject projectilePrefab;

        public Projectile SpawnProjectile(Transform rightHand)
        {
            GameObject projectileGameObject = Instantiate(projectilePrefab, rightHand);
            projectileGameObject.transform.parent = null;
            return projectileGameObject.GetComponent<Projectile>();
        }
    }
}
