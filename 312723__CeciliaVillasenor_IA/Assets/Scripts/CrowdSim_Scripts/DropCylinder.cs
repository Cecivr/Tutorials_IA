using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCylinder : MonoBehaviour
{
    public GameObject obstacle;
    GameObject[] people;
    void Start()
    {
        people = GameObject.FindGameObjectsWithTag("person");   
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                Instantiate(obstacle, hitInfo.point, Quaternion.identity);
                foreach (GameObject a in people)
                {
                    a.GetComponent<AI_Cont_Fleeing>().DetectNewObstacle(hitInfo.point);
                }
            }
        }
    }
}
