using RPG.Movement;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        private Transform target;
        private Mover mover;
        private Animator animator;

        [SerializeField] private float weaponRange = 2f;


        
        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if(target != null)
            {
                mover.MoveTo(target.position, weaponRange);
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            StartCoroutine(TriggerAttackAnimation(combatTarget.transform.position));
        }

        private IEnumerator TriggerAttackAnimation(Vector3 enemyPosition)
        {
            while(Vector3.Distance(transform.position, enemyPosition) > weaponRange)
            {
                yield return null;
            }
            animator.SetTrigger("attack");
        }

        public void Cancel()
        {
            target = null;
        }
        // Animation event
        void Hit()
        {

        }
    }
}
