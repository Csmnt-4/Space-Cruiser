using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Hoop : MonoBehaviour
{
    public GameObject linePrefab;
    public List<GameObject> lines;
    public float distanceoffset;

    private float speed;

    public void Initialize(float speed, float distanceoffset, List<Vector3> positionsNew, List<Vector3> positionsOld)
    {
        this.distanceoffset = distanceoffset;
        this.speed = speed;
        
        for (int i = 0; i < 8; i++)
        {
            GameObject line = Instantiate(linePrefab, positionsNew.ElementAt(i), Quaternion.identity);
            line.GetComponent<LineController>().CreateLine(positionsNew.ElementAt(i), positionsOld.ElementAt(i));
            lines.Add(line);
        }
    }

    void Update()
    {
        float zOffset = speed * Time.deltaTime;

        foreach (var line in lines)
        {
            line.GetComponent<LineController>().UpdateLine(zOffset);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-zOffset);
    }

    public bool ToBeDeleted()
    {
        if (transform.position.z + 200 < 0)
        {
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle collision with player
        }
    }
}
