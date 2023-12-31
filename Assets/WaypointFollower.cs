using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WaypointFollower : MonoBehaviour
{
    
    [SerializeField] GameObject[] waypoints;
    [SerializeField] float speed = 1f;
    int currentWaypoint = 0;
    public GameObject enemy;
    float rotateSpeed = 1f;
    private void Start()
    {
    
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.1f) {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length) { 
                currentWaypoint= 0; 
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);

        
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            Vector3 waypointPos = waypoints[currentWaypoint].transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(waypointPos - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);
        }

    }
}
