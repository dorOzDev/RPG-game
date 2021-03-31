using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        // Start is called before the first frame update

        private NavMeshAgent meshAgent;
        private Animator animator;

        void Start()
        {
            meshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = meshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }


        public void MoveTo(Vector3 dest)
        {
            meshAgent.destination = dest;
        }
    }

}

