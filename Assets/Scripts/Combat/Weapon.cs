using RPG.Characters;
using RPG.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Stats;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponData weaponData;
        protected virtual void OnTriggerEnter(Collider other)
        {
            BaseCharacter hitChar = other.GetComponentInParent<BaseCharacter>();
            BaseCharacter self = GetComponentInParent<BaseCharacter>();
            BaseStats stats = GetComponentInParent<BaseStats>();

            if(hitChar != null && self != null)
            {
                if(hitChar.CharType != self.CharType)
                {
                    print(self.gameObject.name + " attacking: " + other.gameObject.name + " with " + stats.GetStat(Stat.Damage) + " damage");
                    hitChar.TakeDamage(weaponData.Damage);
                }
            }
        }
    }
}
