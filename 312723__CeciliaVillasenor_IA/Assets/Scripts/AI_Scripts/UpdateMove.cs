using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMove : MonoBehaviour
{
    public float speed = 5f;
    void Update()
    {
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
