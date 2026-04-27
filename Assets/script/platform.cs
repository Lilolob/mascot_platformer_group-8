using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class platform : MonoBehaviour
{

    public List<GameObject> Waypoints;
    public float speed = 2;
    int index = 0;

    public void Start()
    {
        transform.position = Waypoints[index].transform.position;
    }

    void Update()
    {

        Vector3 destintion = Waypoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, Waypoints[index].transform.position, speed * Time.deltaTime);
        transform.position = newPos;
        float distance = Vector3.Distance(transform.position, destintion);
        Vector3 targetDir = destintion - transform.position;
        targetDir.y = 0.0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * 50);
        if (transform.position == Waypoints[index].transform.position)
        {
            index += 1;
        }
        if (index == Waypoints.Count)
        {
            index = 0;
            Waypoints.Reverse();
        }
    }
}