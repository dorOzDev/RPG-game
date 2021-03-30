using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshAgent meshAgent;
    private Animator animator;

    private Ray ray;
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {        
            MoveToClick();
        }
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = meshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);
    }

    private void MoveToClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) 
        {
            meshAgent.destination = hit.point;
        }

    }
}
