using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                Fighter fighter = other.gameObject.GetComponent<Fighter>();
                fighter.EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}
