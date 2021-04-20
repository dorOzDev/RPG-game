using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;



namespace RPG.Movement
{
    public class Mover : MonoBehaviour, ISaveable
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

        public void EnableMover()
        {
            meshAgent.enabled = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = meshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }


        public void MoveTo(Vector3 dest, float speed ,float stoppingDistance)
        {
            meshAgent.speed = speed;
            meshAgent.stoppingDistance = stoppingDistance;
            meshAgent.destination = dest;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            Vector3 savedPosition = (state as SerializableVector3).ToVector();
            // Mesh agent may cause issues when setting the position that way, hence first disabling it before reosition and then reenable it.
            DisableMover();
            transform.position = savedPosition;
            EnableMover();
        }
    }
}

