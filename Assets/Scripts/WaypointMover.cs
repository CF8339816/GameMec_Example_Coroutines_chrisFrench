using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class WaypointMover : MonoBehaviour
{


    Rigidbody rb;
    GameObject WayPointPlatform;
    List<Vector3> _waypoints = new List<Vector3>();
   
    //Vector3 startPosition;
    //Vector3 endPosition;
    //Vector3 targetPosition;
    
    [SerializeField]  float _speed;
    
    //[SerializeField] float smoothTime = 0.5f;
    //[SerializeField] float stoppingDistance = 0.1f;
    //[SerializeField] Vector3 currentTarget;
    //[SerializeField] Vector3 currentVelocity = Vector3.zero;
    //[SerializeField] float moveDuration = 4.0f;

    // Add the position of all children tagged "Waypoint" to the _waypoints list
    // Also sets the position of the platform to the position of the 1st waypoint.
    private void Start()
    {
        //startPosition = transform.position;
        //targetPosition = gizmoWaypoints;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        foreach (Transform childTransform in this.transform)
        {
            if (!childTransform.CompareTag("Waypoint")) continue;

            _waypoints.Add(childTransform.position);

        }

        if (_waypoints.Count == 0) return;


        transform.position = _waypoints[0];

        if (_waypoints.Count > 1)
        {
            //TODO: once MoveBetweenWayPoints method exists, uncomment the line below. 
            StartCoroutine(MoveBetweenWayPoints());
        }



    }

    // COROUTINES: 
    // A coroutine is a nice way to run some code over time, allowing you to pause in the middle of a method and then resume later!
    // All coroutine methods must have a return type of IEnumerator. You run them by using StartCoroutine(MyCoroutineMethod())

    // Challenge: 
    // Create an IEnumerator MoveBetweenWayPoints below. 

    // The coroutine should make the platform move to the waypoints in order.
    // At each waypoint, wait for 1 second. Hint: WaitForSecondsRealTime(1f)

    // useful methods: Vector3.MoveTowards, rb.MovePosition

    // The solution should only be approximately 15-20 lines of code or less (not including bracket lines and whitespace). 

    IEnumerator MoveBetweenWayPoints()
    {

        //  while //(waypoint platform position != waypoint position)
        //   { 


        //float t = Mathf.PingPong(Time.time, moveDuration) / moveDuration; // gets movement value using PingPong math 
        //transform.position = Vector3.Lerp(startPosition, endPosition, t); // lerp using pingpong value for t

        // move platform toward waypoint position 

        // }


        //if (Vector3.Distance(transform.position, currentTarget) < stoppingDistance)
        //{
        //    // target switch for return trip check
        //    if (currentTarget == targetPosition)
        //    {
        //        currentTarget = startPosition;
        //    }
        //    else
        //    {
        //        currentTarget = targetPosition;
        //    }
        //}

        //transform.position = Vector3.SmoothDamp(transform.position, currentTarget, ref currentVelocity, smoothTime);


        yield return new WaitForSeconds(1.0f);

        // WaypointPlatform move towards next waypoint position





    }


    private void OnDrawGizmos()
    {

        if (Application.isPlaying) return;

        List<Vector3> gizmoWaypoints = new List<Vector3>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Waypoint"))
                gizmoWaypoints.Add(child.position);
        }

        if (gizmoWaypoints.Count == 0) return;

        Gizmos.color = Color.green;

        for (int i = 0; i < gizmoWaypoints.Count; i++)
        {
            Gizmos.DrawSphere(gizmoWaypoints[i], 0.2f);

            if (i < gizmoWaypoints.Count - 1)
            {
                Gizmos.DrawLine(gizmoWaypoints[i], gizmoWaypoints[i + 1]);
            }
        }
    }




}
