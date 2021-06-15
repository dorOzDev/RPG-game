using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] WeaponData weapon;
        [SerializeField] float respawnTime = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                Fighter fighter = other.gameObject.GetComponent<Fighter>();
                fighter.EquipWeapon(weapon);
                StartCoroutine(HideForSeconds());
            }
        }

        private IEnumerator HideForSeconds()
        {
        
            SetPickupVisibility(false);
            yield return new WaitForSeconds(respawnTime);
            SetPickupVisibility(true);
        }

        private void SetPickupVisibility(bool isVisible)
        {
            // First set the collider.
            GetComponent<Collider>().enabled = isVisible;
            // Than set all of its chilren activity
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(isVisible);
            }
            
        }
    }
}
