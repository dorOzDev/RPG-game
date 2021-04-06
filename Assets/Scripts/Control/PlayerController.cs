using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Characters;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Fighter fighter;
        private Mover mover;
        private Player player;
        private const float stopWalkingDistance = 0;
        void Start()
        {
            player = GetComponent<Player>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        void Update()
        {
            if (!player.IsAlive) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                BaseCharacter combatTarget = hit.transform.gameObject.GetComponent<Enemy>();
                
                if (combatTarget == null) continue;

                if (Input.GetMouseButtonDown(0)) fighter.Attack(combatTarget);

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                if (Input.GetMouseButton(0))
                {
                    fighter.CancelAttack();
                    mover.MoveTo(hit.point, stopWalkingDistance);
                }

                return true;
            }

            return false;
        }
    }
}
