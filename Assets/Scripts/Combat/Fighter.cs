using RPG.Movement;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        private CombatTarget target;
        private Mover mover;
        private Animator animator;

        [SerializeField] private float damage = 3f;

        [SerializeField] private float weaponRange = 2f;

        [SerializeField] private float timeBetweenAttacks = 3f;

        private bool isInRest = false;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            
            if (target != null)
            {
                mover.MoveTo(target.transform.position, weaponRange);
                
                StartCoroutine(TriggetAttackAnimation(target.transform.position));
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget;
        }

        private IEnumerator StartRestBetweenAttacksCountDown()
        {
            float totalTime = 0;
            while(isInRest && totalTime <= timeBetweenAttacks)
            {
                totalTime += Time.deltaTime;
                yield return null;
            }

            isInRest = false;
        }

        private IEnumerator TriggetAttackAnimation(Vector3 enemyPosition)
        {
            while(Vector3.Distance(transform.position, enemyPosition) > weaponRange)
            {
                yield return null;
            }

            if (!isInRest) 
            {
                animator.SetTrigger("attack");
                isInRest = true;
                StartCoroutine(StartRestBetweenAttacksCountDown());
            }
        }

        public void Cancel()
        {
            target = null;
        }
        // Animation event(is called once the animator SetsTrigget of "attack"
        void Hit()
        {
            target.TakeDamage(damage);
        }
    }
}
