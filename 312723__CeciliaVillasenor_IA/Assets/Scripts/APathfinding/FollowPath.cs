using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    float speed = 5;
    float accuracy = 1;
    float rotSpeed = 2;
    public GameObject wpManager;
    GameObject[] wpts;
    GameObject currentNode;
    int currentWP = 0;
    Graph gph;
    void Start()
    {
        wpts = wpManager.GetComponent<WPManager>().waypoints;
        gph = wpManager.GetComponent<WPManager>().graph;
        currentNode = wpts[0];
    }
    public void GoToHeli()
    {
        gph.AStar(currentNode, wpts[1]);
        currentWP = 0;
    }
    public void GoToRuin()
    {
        gph.AStar(currentNode, wpts[6]);
        currentWP = 0;
    }
    public void GoToTank()
    {
        gph.AStar(currentNode, wpts[8]);
        currentWP = 0;
    }
    void LateUpdate()
    {
        if(gph.getPathLength() == 0 || currentWP == gph.getPathLength())
        {
            return;
        }

        currentNode = gph.getPathPoint(currentWP);
        
        if(Vector3.Distance(
            gph.getPathPoint(currentWP).transform.position,
            transform.position) < accuracy)
        {
            currentWP++;
        }

        if(currentWP < gph.getPathLength())
        {
            goal = gph.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,
                                             this.transform.position.y,
                                             goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
