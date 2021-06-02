using RPG.Characters;
using RPG.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if(hitChar != null && self != null)
            {
                if(hitChar.CharType != self.CharType)
                {
                    hitChar.TakeDamage(weaponData.Damage);
                }
            }
        }
    }
}
