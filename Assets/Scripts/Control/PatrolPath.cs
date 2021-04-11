using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control 
{
    public class PatrolPath : MonoBehaviour
    {

        [SerializeField]
        private float sphereRadius = 0.5f;

        private int currPatrolPathIndex = 0;


        private void Awake()
        {
            // If no child is available than add a single empty child. Simply to avoid null checks in other places.
            if (transform.childCount == 0) AddSingleWayPoint();
        }

        private void AddSingleWayPoint()
        {
            GameObject emptyWayPoint = new GameObject("Waypoint");

            emptyWayPoint.transform.SetParent(transform);

            emptyWayPoint.transform.localPosition = Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            int childCount = transform.childCount;
            // First draw Sphere on the source child
            for (int i = 0; i < childCount; ++i)
            {
                Transform childTranform = transform.GetChild(i);
 
                Gizmos.DrawSphere(childTranform.position, sphereRadius);
            }

            // Than draw a line between the source and dest child
            for (int i = 0; i < childCount; ++i)
            {
                Transform fromTransform = transform.GetChild(i);
                Transform toTransform = transform.GetChild((i + 1) % childCount);

                Gizmos.DrawLine(fromTransform.position, toTransform.position);
            }
        }

        public Vector3 GetNextWayPoint()
        {
            int wayPointIndex = (currPatrolPathIndex) % transform.childCount;

            ++currPatrolPathIndex;

            return transform.GetChild(wayPointIndex).position;
        }
    }
}
