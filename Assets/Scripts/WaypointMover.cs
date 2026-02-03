using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class WaypointMover : MonoBehaviour
{

    public enum MovementType {Loop, PingPong} //sets up the enum for switch select between the two movement choices.
    [SerializeField] MovementType _movementType; //sets up the user interaction to select the movement type
   
    Rigidbody rb;  //Provided code
    GameObject WayPointPlatform;//Provided code

    List<Vector3> _waypoints = new List<Vector3>()//Provided code

    [SerializeField] float _speed;//Provided code


    private void Start()
    {
  
        rb = GetComponent<Rigidbody>();//Provided code
        rb.isKinematic = true;//Provided code

        foreach (Transform childTransform in this.transform)//Provided code
        {
            if (!childTransform.CompareTag("Waypoint"))//Provided code
            {
                continue;
            }
            _waypoints.Add(childTransform.position);//Provided code
        }

        if (_waypoints.Count == 0)//Provided code
        {
            return;
        }
        transform.position = _waypoints[0];//Provided code

        if (_waypoints.Count > 1)//Provided code
        {
            StartCoroutine(MoveBetweenWayPoints());
        }

        switch (_movementType)// the switch to set up rthe movement type callouts once selected
        {
            case MovementType.Loop:// nstandard looping animation
                StartCoroutine(MoveBetweenWayPoints());
                break;
            case MovementType.PingPong:  // standart loop to the end point then reverses back to the start then begins loop again
                StartCoroutine(PingPong());
                break;
        }
    }
       
    IEnumerator MoveBetweenWayPoints() //initial  move between waypoints coroutine
    {
        int currentIndex = 0;// start at first position
        while (true)
        {
            yield return MoveToTarget(_waypoints[currentIndex]); // sets a stop to recieve info from the movemnt enumerator
            yield return new WaitForSeconds(1.0f);   // sets a delay of 1 second at each waypoint
            currentIndex = (currentIndex + 1) % _waypoints.Count; // sets up the  next step in the path by moving the waypoint to
                                                                  // the next one in the list
        }
    }
        
    IEnumerator PingPong() // initalizes the  ping pong front to back anc back to front movement  coroutine
    {
        int currentIndex = 0; //start at ffirst position 
        int direction = 1; //sets direction to movve  Pos forward neg for backward
        while (true)
        {
            yield return MoveToTarget(_waypoints[currentIndex]); // sets a stop to recieve info from the movemnt enumerator
            yield return new WaitForSeconds(1.0f); // sets a delay of 1 second at each waypoint

            if (currentIndex == _waypoints.Count - 1) direction = -1; // checks if the movement is on its way back  and sets next
                                                                      // to the previous one in  the list 
            else if (currentIndex == 0) direction = 1; // checks if the starting waytpoint is the first one in the list  and moves
                                                       // the destination tho the next  waypoiontin the list

            currentIndex += direction;
        }
    }
    
    IEnumerator MoveToTarget(Vector3 target)// sets upo the move to the next  position defined enumerator  coroutine
    {
        while (Vector3.Distance(rb.position, target) > 0.01f)// checks for closeness to target for easement
        {
            Vector3 nextPos = Vector3.MoveTowards(rb.position, target, _speed * Time.deltaTime); //formuila to move to next position
            rb.MovePosition(nextPos); // sets the move position to next position
            yield return new WaitForFixedUpdate(); // caused a break in loop to wait on the fixed update before continuing loop
        }
    }

    private void OnDrawGizmos()  //Provided code
    {

        if (Application.isPlaying) return;//Provided code

        List<Vector3> gizmoWaypoints = new List<Vector3>();//Provided code to make the vector list for  waypoints

        foreach (Transform child in transform)//Provided code
        {
            if (child.CompareTag("Waypoint"))//Provided code
            {
                gizmoWaypoints.Add(child.position);
            }
        }

        if (gizmoWaypoints.Count == 0) return;//Provided code

        Gizmos.color = Color.green;//Provided code

        for (int i = 0; i < gizmoWaypoints.Count; i++)//Provided code
        {
            Gizmos.DrawSphere(gizmoWaypoints[i], 0.2f);//Provided code

            if (i < gizmoWaypoints.Count - 1)//Provided code
            {
                Gizmos.DrawLine(gizmoWaypoints[i], gizmoWaypoints[i + 1]);
            }
        }
    }

}
