using RPG.Core;
using UnityEngine;
using UnityEngine.AI;



namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        // Start is called before the first frame update

        private NavMeshAgent meshAgent;
        private Animator animator;

        private void Awake()
        {
            meshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void DisableMover()
        {
            meshAgent.enabled = false;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = meshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }


        public void MoveTo(Vector3 dest, float stoppingDistance)
        {
            meshAgent.stoppingDistance = stoppingDistance;
            meshAgent.destination = dest;
        }
    }
}

