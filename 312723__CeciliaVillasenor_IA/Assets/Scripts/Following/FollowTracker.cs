using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTracker : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP;

    [Range(1,100)]
    public float speed = 10.0f;
    public float rotSpeed = 2f;
    public float lookAhead = 10;
    GameObject tracker;
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;
    }
    void ProgressTracker()
    {
        if(Vector3.Distance(tracker.transform.position, this.transform.position) > lookAhead)
        {
            return;
        }
        if(Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3)
        {
            currentWP++;
        }
        if(currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }
        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, (speed + 2) * Time.deltaTime);
    }
    void Update()
    {
        /*if(Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 3)
            currentWP++;

         if(currentWP >= waypoints.Length)
            currentWP = 0;*/

        ProgressTracker();

        Quaternion lookatWP = Quaternion.LookRotation(tracker.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWP, rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, speed * Time.deltaTime);    
    }
}
